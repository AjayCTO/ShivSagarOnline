
using SHIVAM_ECommerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHIVAM_ECommerce.Models;
using SHIVAM_ECommerce.Repository;
using SHIVAM_ECommerce.Functions;
using System.Data.OleDb;
using System.Data;
using LinqToExcel;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Configuration;
using System.Data.SqlClient;
using SHIVAM_ECommerce.Attributes;
namespace SHIVAM_ECommerce.Controllers
{
    [CustomAuthorize]
    public class ProductController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IRepository<Product> _repository = null;
        private IRepository<ProductImages> _Imagesrepository = null;
        private IRepository<ProductAttributeWithQuantity> _Attributerepository = null;
        public List<object> newcolumns = null;
        public List<string> _columns = new List<string>();
        public int SupplierId = 0;
        public ProductController()
        {
            this._repository = new Repository<Product>();
            this._Imagesrepository = new Repository<ProductImages>();
            this._Attributerepository = new Repository<ProductAttributeWithQuantity>();
            this.newcolumns = new List<object>();



        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        public FileStreamResult DownloadSample()
        {
            return ExportToExcel.getSampleFile(CurrentUserData.SupplierID);

        }

        public ActionResult ImportExport()
        {
            return View();
        }

        public FileStreamResult ExportData()
        {
            return ExportToExcel.getExcelData("select * from suppliers", "Supplier");

        }

        private IEnumerable<object[]> Read(DbDataReader reader)
        {
            var k = 0;
            while (reader.Read())
            {

                var values = new List<object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (k == 0)
                    {
                        newcolumns.Add(reader.GetName(i));
                    }


                    values.Add(reader.GetValue(i));
                }
                k++;
                yield return values.ToArray();
            }
        }

        public ActionResult GetAllProducts()
        {
            _columns = ExportToExcel.GetCustomColumnNames(SupplierId);
            SupplierId = CurrentUserData.SupplierID;
            var ctx = new ApplicationDbContext();
            var _supplierdata = ctx.Suppliers.FirstOrDefault();
            SupplierId = SupplierId == -1 ? (_supplierdata != null ? _supplierdata.Id : -1) : SupplierId;
            ViewBag.SupplierID = SupplierId;
            using (var cmd = ctx.Database.Connection.CreateCommand())
            {
                ctx.Database.Connection.Open();
                cmd.CommandText = "EXEC SP_GetAllSortedProducts 5,0,'productName','ASC',NULL, " + SupplierId + "";

                var _xdta = cmd.ExecuteReader();
                if (_xdta != null)
                {

                    using (var reader = _xdta)
                    {
                        var model = Read(reader).ToList();
                        TempData["cols"] = newcolumns;
                        return View(model);
                    }
                }
            }

            return View(new List<object>());
        }


        public ActionResult GetColumnNames(int SupplierID)
        {
            try
            {
                SupplierID = CurrentUserData.IsSuperAdmin == true ? SupplierID : CurrentUserData.SupplierID;
                var ctx = new ApplicationDbContext();
                var _List = new List<object>();
                using (var cmd = ctx.Database.Connection.CreateCommand())
                {
                    ctx.Database.Connection.Open();
                    cmd.CommandText = "EXEC SP_GetAllSortedProducts 5,0,'productName','ASC',NULL, " + SupplierID + "";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var model = Read(reader).ToList();
                        _List = newcolumns;
                        return Json(new { Success = true, Data = _List, ex = "" }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {

                return Json(new { Success = true, Data = "", ex = ex.Message.ToString(), JsonRequestBehavior.AllowGet });
            }
        }
        private string GetAttributeValuesForSql(Row a, List<ProductAttributesRelation> Columns)
        {
            var _string = "";


            foreach (var _columnData in Columns)
            {

                _string += _columnData.ProductAttributesId.ToString() + "@@" + a[_columnData.ProductAttributes.AttributeName] + "##";

            }


            return _string;
        }

        [HttpPost]
        public JsonResult UploadExcel()
        {

            try
            {

                SupplierId = CurrentUserData.SupplierID;
                string _ErrorString = "";
                var FileUpload = Request.Files["FileUpload"];
                List<string> data = new List<string>();
                if (FileUpload != null)
                {
                    if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {


                        string filename = FileUpload.FileName;
                        string targetpath = Server.MapPath("~/ProductImages/");
                        FileUpload.SaveAs(targetpath + filename);
                        string pathToExcelFile = targetpath + filename;
                        var connectionString = "";
                        if (filename.EndsWith(".xls"))
                        {
                            // connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=\"Excel 8.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                            //connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.4.0;Data Source={0}; Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\";", pathToExcelFile);
                            connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathToExcelFile + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
                            // connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathToExcelFile + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";

                        }
                        else if (filename.EndsWith(".xlsx"))
                        {
                            connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                        }
                        OleDbConnection oledbconnection = new OleDbConnection(connectionString);
                        oledbconnection.Open();
                        DataTable Sheets = oledbconnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string sht = "";


                        String[] excelSheets = new String[Sheets.Rows.Count];
                        int i = 0;

                        // Add the sheet name to the string array.
                        foreach (DataRow row in Sheets.Rows)
                        {
                            excelSheets[i] = row["TABLE_NAME"].ToString();
                            i++;
                        }

                        sht = excelSheets[0];
                        oledbconnection.Close();
                        var adapter = new OleDbDataAdapter("SELECT * FROM [" + sht + "]", connectionString);
                        var ds = new DataSet();

                        adapter.Fill(ds, "ExcelTable");

                        DataTable dtable = ds.Tables["ExcelTable"];

                        var excelFile = new ExcelQueryFactory(pathToExcelFile);
                        var worksheet = excelFile.GetWorksheetNames();
                        var columnNames = excelFile.GetColumnNames(worksheet.FirstOrDefault());

                        var records = from a in excelFile.Worksheet(0) select a;

                        List<ImportProductVM> allProducts = new List<ImportProductVM>();
                        var _count = 0;
                        foreach (var a in records)
                        {


                            var _validityChecker = IsValidObject(a, _count + 1);
                            if (!_validityChecker.IsValid)
                            {

                                _ErrorString = _ErrorString + "<br/>" + _validityChecker.ErrorString;

                            }

                            _count = _count + 1;
                        }

                        if (_ErrorString.Length > 0)
                        {
                            return Json(new { Success = (_ErrorString.Length > 0) ? false : true, ErrorString = _ErrorString }, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            var _Customcolumns = ExportToExcel.GetCustomColumns(SupplierId);
                            var NewconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                            using (SqlConnection connection = new SqlConnection(NewconnectionString))
                            {

                                foreach (var a in records)
                                {

                                    string _Query = "DECLARE @return_value int,@ProductID int;";
                                    _Query += "SELECT	@ProductID = -1;";
                                    _Query += "EXEC	@return_value = [dbo].[InsertProduct] @Name='" + a["Name"] + "',@ProductCode = '" + a["ProductCode"] + "',@Description = '" + a["Description"] + "',@categoryName = '" + a["categoryName"] + "',@UnitOfMeasure = '" + a["UnitOfMeasure"] + "',@ManuFacturer = '" + a["ManuFacturer"] + "',@SupplierID = '" + SupplierId + "',@ProductID = @ProductID OUTPUT;";
                                    _Query += "SELECT	@ProductID as N'ProductID'";
                                    SqlCommand command = new SqlCommand(_Query, connection);
                                    command.CommandTimeout = 640;
                                    command.Connection.Open();
                                    var _productID = command.ExecuteScalar();

                                    _Query = "EXEC [dbo].[InsertProductAttributes] @AttributeValues='" + GetAttributeValuesForSql(a, _Customcolumns) + "', @ProductPrice = '" + a["Cost"] + "', @ProductQuantity = '" + a["Quantity"] + "', @ProductId = '" + _productID.ToString() + "', @UnitInStock = '" + a["Quantity"] + "', @UnitWeight = '0',@HighQuantityThreshold='" + a["HighQuantityThreshold"] + "',@LowQuantityThreshold='" + a["LowQuantityThreshold"] + "',@ProductStatus='" + a["ProductStatus"] + "';";

                                    command = new SqlCommand(_Query, connection);
                                    command.ExecuteNonQuery();

                                    command.CommandTimeout = 640;
                                    command.Connection.Close();
                                }

                            }

                        }
                        //deleting excel file from folder  
                        if ((System.IO.File.Exists(pathToExcelFile)))
                        {
                            System.IO.File.Delete(pathToExcelFile);
                        }

                        return Json(new { Success = (_ErrorString.Length > 0) ? false : true, ErrorString = _ErrorString }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        _ErrorString = "Only Excel file format is allowed";

                        return Json(new { Success = (_ErrorString.Length > 0) ? false : true, ErrorString = _ErrorString }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    _ErrorString = "Please Choose Excel File";

                    return Json(new { Success = (_ErrorString.Length > 0) ? false : true, ErrorString = _ErrorString }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { Success = false, ErrorString = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        private ImportValid IsValidObject(Row a, int _Index)
        {
            var _ValidObj = new ImportValid();
            _ValidObj.IsValid = true;
            _ValidObj.ErrorString = "";
            try
            {


                if (string.IsNullOrEmpty(a["Name"].ToString())) _ValidObj.ErrorString += "<br/> Product name is Required";
                if (string.IsNullOrEmpty(a["categoryName"].ToString())) _ValidObj.ErrorString += "<br/> category name is Required";
                if (string.IsNullOrEmpty(a["UnitOfMeasure"].ToString())) _ValidObj.ErrorString += "<br/> UnitofMasure is Required";
                if (string.IsNullOrEmpty(a["ManuFacturer"].ToString())) _ValidObj.ErrorString += "<br/> Manufacturer name is Required";
                if (string.IsNullOrEmpty(a["HighQuantityThreshold"].ToString())) _ValidObj.ErrorString += "<br/> High Quantity Threshold is Required";
                if (string.IsNullOrEmpty(a["LowQuantityThreshold"].ToString())) _ValidObj.ErrorString += "<br/>  Low Quantity Threshold is Required";
                if (string.IsNullOrEmpty(a["ProductStatus"].ToString())) _ValidObj.ErrorString += "<br/> Product Status is Required";

                foreach (var item in _columns.ToList())
                {
                    var _column = item.ToString();
                    decimal _checkDecimal = -1;
                    if (string.IsNullOrEmpty(a[_column].Value.ToString())) _ValidObj.ErrorString += "<br/> " + _column + " is Required";

                    if (!string.IsNullOrEmpty(a[_column].Value.ToString()) && (_column.ToLower() == "quantity" || _column.ToLower() == "cost" || _column.ToLower() == "HighQuantityThreshold" || _column.ToLower() == "LowQuantityThreshold"))
                    {
                        if (!decimal.TryParse(a[_column].Value.ToString(), out _checkDecimal)) _ValidObj.ErrorString += "<br/> " + _column + " is not a valid number";
                    }
                }

                if (_ValidObj.ErrorString.Length > 0)
                {
                    _ValidObj.IsValid = false;
                    _ValidObj.ErrorString = "<b>Row " + _Index.ToString() + "</b>" + _ValidObj.ErrorString;

                }
                return _ValidObj;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult AssignProductImages(int ProductID)
        {
            ViewBag.ProductID = ProductID;
            var _db = new ApplicationDbContext();
            var _Data = _db.ProductImages.Where(x => x.ProductQuantityId == ProductID).Select(x => x.ImageName).ToList();
            ViewBag.AlreadySelected = _Data;
            return View();
        }


        public ActionResult Create()
        {
            return View(new ProductViewmodel());
        }

        [HttpPost]
        public ActionResult Create(ProductViewmodel Model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    #region Product Save
                    var _product = new Product();
                    _product.ManuFacturerID = Model.ManufacturerID;
                    _product.ProductCode = Model.ProductCode;
                    _product.ProductName = Model.ProductName;
                    _product.CateogryID = Model.CategoryID;
                    _product.SupplierID = Model.SupplierID;
                    _product.ManuFacturerID = Model.ManufacturerID;
                    _product.SKU = Model.SKU;
                    _product.CreatedDate = DateTime.Now;
                    _product.UpdatedDate = DateTime.Now;
                    _product.UnitOfMeasuresId = Model.UnitOfMeasureID;
                    _product.Description = Model.Description;
                    _repository.Insert(_product);
                    _repository.Save();
                    #endregion


                    return Json(new { Success = true, ex = "", data = "", id = _product.Id });
                }
                catch (Exception ex)
                {

                    return Json(new { Success = false, ex = ex.Message.ToString(), data = "" });

                }

            }

            return Json(new { Success = true, ex = "", data = "" });
        }
        public ActionResult Edit(int productID)
        {
            return View(GetProduct(productID));
        }

        [HttpPost]
        public ActionResult Edit(ProductViewmodel Model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    #region Product Save
                    var _product = _repository.GetById(Model.ProductID);
                    _product.ManuFacturerID = Model.ManufacturerID;
                    _product.ProductCode = Model.ProductCode;
                    _product.ProductName = Model.ProductName;
                    _product.CateogryID = Model.CategoryID;
                    _product.SupplierID = Model.SupplierID;
                    _product.ManuFacturerID = Model.ManufacturerID;
                    _product.SKU = Model.SKU;
                    _product.UpdatedDate = DateTime.Now;
                    _product.UnitOfMeasuresId = Model.UnitOfMeasureID;
                    _product.Description = Model.Description;
                    _repository.Save();
                    #endregion


                    return Json(new { Success = true, ex = "", data = "", id = _product.Id });
                }
                catch (Exception ex)
                {

                    return Json(new { Success = false, ex = ex.Message.ToString(), data = "" });

                }

            }

            return Json(new { Success = true, ex = "", data = "" });
        }
        public ProductViewmodel GetProduct(int productID)
        {
            var _ProductViewModel = new ProductViewmodel();
            var _Product = _repository.GetById(productID);
            _ProductViewModel.ProductID = _Product.Id;
            _ProductViewModel.SupplierID = _Product.SupplierID;
            _ProductViewModel.UnitOfMeasureID = _Product.UnitOfMeasuresId;
            _ProductViewModel.ManufacturerID = _Product.ManuFacturerID;
            _ProductViewModel.SKU = _Product.SKU;
            _ProductViewModel.CategoryID = _Product.CateogryID;
            _ProductViewModel.ProductName = _Product.ProductName;
            _ProductViewModel.ProductCode = _Product.ProductCode;
            _ProductViewModel.Description = _Product.Description;
            _ProductViewModel.IDSKU = _Product.IDSKU;

            return _ProductViewModel;
        }
        public ActionResult ProductDetails(int productID)
        {
            var productstauts = db.ProductStatus.ToList();
            ViewBag.productstatuslist = productstauts;

            var _ProductViewModel = GetProduct(productID);
            var _ProductAttributes = _Attributerepository.GetAll().Where(x => x.ProductId == productID && x.IsActive == true).ToList();
            _ProductViewModel.allAttributes = new List<ProductAttributeModel>();
            foreach (var _item in _ProductAttributes)
            {
                var _newitem = new ProductAttributeModel();
                _newitem.price = _item.ProductPrice;
                _newitem.weight = _item.UnitWeight;
                _newitem.Quantity = _item.ProductQuantity;
                _newitem.highQuantityThreshold = _item.highQuantityThreshold;
                _newitem.lowQuantityThreshold = _item.lowQuantityThreshold;
                _newitem.IsFeatured = _item.IsFeatured;
                _newitem.StatusId = _item.StatusId;
                _newitem.ColumnsData = new List<ProductAttributeModelInner>();
                _newitem.ColumnsData = GetColumnsDataSplitted(_item.AttributeValues);
                _newitem.Images = new List<ProductImagesViewModel>();
                _newitem.Images = GetImages(_item.Id);
                _newitem.Id = _item.Id;
                _ProductViewModel.allAttributes.Add(_newitem);
            }

            return View(_ProductViewModel);
        }

        private List<ProductImagesViewModel> GetImages(int ProductID)
        {
            var _listImages = _Imagesrepository.GetProductImages(ProductID);
            var _productImages = new List<ProductImagesViewModel>();
            var _count = 0;
            var _Path = "";
            foreach (var _item in _listImages)
            {
                _count += 1;
                var _Newitem = new ProductImagesViewModel();
                _Newitem.ImageID = _count;
                _Newitem.FileName = _item.ImagePath;
                _Path = System.Web.Hosting.HostingEnvironment.MapPath("~/ProductImages/") + _item.ImagePath;
                _Newitem.bytestring = System.IO.File.ReadAllBytes(_Path);
                _Newitem.ID = _item.Id;
                _productImages.Add(_Newitem);

            }

            return _productImages;
        }

        [HttpGet]
        public ActionResult GetDataImages(int ProductID)
        {
            try
            {

                var _db = new ApplicationDbContext();
                var _ProductQuantityIds = _db.ProductAttributeWithQuantity.Where(x => x.ProductId == ProductID).Select(x => x.Id);
                var _listImages = _db.ProductImages.Where(x => _ProductQuantityIds.Contains(x.ProductQuantityId)).ToList();
                var _productImages = new List<ProductImagesViewModel>();
                var _count = 0;
                foreach (var _item in _listImages)
                {
                    _count += 1;
                    var _Newitem = new ProductImagesViewModel();
                    _Newitem.ImageID = _count;
                    _Newitem.FileName = _item.ImagePath;
                    _productImages.Add(_Newitem);

                }
                return Json(new { Success = true, ex = "", data = _productImages }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex = ex.Message.ToString(), data = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        private List<ProductAttributeModelInner> GetColumnsDataSplitted(string p)
        {
            string[] parts1 = p.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
            var _ReturnModel = new List<ProductAttributeModelInner>();
            foreach (var _item in parts1.ToList())
            {
                string[] parts2 = _item.Split(new string[] { "@@" }, StringSplitOptions.RemoveEmptyEntries);
                var _attributeID = parts2[0];
                var _value = parts2[1];
                _ReturnModel.Add(new ProductAttributeModelInner { AttributeID = Convert.ToInt16(_attributeID), Value = _value });
            }

            return _ReturnModel;
        }

        [HttpPost]
        public ActionResult ProductDetails(ProductViewmodel Model)
        {

            try
            {

                // DeleteOldAttributes(Model.ProductID);
                var _ProductAttributes = _Attributerepository.GetAll().Where(x => x.ProductId == Model.ProductID).ToList();

                var _productRel = new ProductAttributeWithQuantity();
                #region Product Attributes
                Model.allAttributes = (Model.allAttributes == null ? new List<ProductAttributeModel>() : Model.allAttributes);
                if (Model.allAttributes.Count() > 0)
                {
                    foreach (var _item in Model.allAttributes)
                    {
                        var _tempProductRel = _ProductAttributes != null && _ProductAttributes.Count() > 0 ? _ProductAttributes.Where(x => x.Id == _item.Id).FirstOrDefault() : new ProductAttributeWithQuantity();
                        _tempProductRel = _tempProductRel == null ? new ProductAttributeWithQuantity() : _tempProductRel;
                        _productRel = _tempProductRel;
                        _productRel.IsAvailable = true;
                        _productRel.AttributeValues = GetAttributeValues(_item.ColumnsData);
                        _productRel.ProductPrice = Model.ProductPrice;
                        _productRel.ProductQuantity = _item.Quantity;
                        _productRel.ProductId = Model.ProductID;
                        _productRel.UnitInStock = _item.Quantity;
                        _productRel.UnitWeight = _item.weight;
                        _productRel.StatusId = _item.StatusId;
                        _productRel.IsFeatured = _item.IsFeatured;
                        _productRel.lowQuantityThreshold = _item.lowQuantityThreshold;
                        _productRel.highQuantityThreshold = _item.highQuantityThreshold;
                        _productRel.IsActive = true;
                        if (_productRel.Id == 0)
                        {
                            _Attributerepository.Insert(_productRel);

                        }
                        _Attributerepository.Save();

                        #region Product Image
                        _item.Images = (_item.Images == null ? new List<ProductImagesViewModel>() : _item.Images);
                        if (_item.Images.Count() > 0)
                        {
                            foreach (var _image in _item.Images)
                            {
                                if (_image.ID == 0)
                                {

                                    byte[] bitmap = _image.bytestring;
                                    var _imageName = "";
                                    if (bitmap != null)
                                    {
                                        _imageName = Guid.NewGuid().ToString() + ".png";

                                        var _Path = System.Web.Hosting.HostingEnvironment.MapPath("~/ProductImages/");
                                        using (Image image = Image.FromStream(new MemoryStream(bitmap)))
                                        {
                                            var _CompletePath = _Path + _imageName;
                                            image.Save(_CompletePath, ImageFormat.Png);  // Or Png
                                        }
                                    }

                                    var _productImage = new ProductImages();
                                    _productImage.ImageName = _image.FileName;
                                    _productImage.ImagePath = _imageName;
                                    _productImage.ProductQuantityId = _productRel.Id;
                                    //_productImage.ProductId = Model.ProductID;
                                    _productImage.CreatedDate = DateTime.Now;
                                    _productImage.UpdatedDate = DateTime.Now;
                                    _Imagesrepository.Insert(_productImage);
                                    _Imagesrepository.Save();
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion



                return Json(new { Success = true, ex = "", data = "" });
            }
            catch (Exception ex)
            {

                return Json(new { Success = false, ex = ex.Message.ToString(), data = "" });

            }

        }
        public FileStreamResult ExportProducts()
        {
            return ExportToExcel.getExcelData("EXEC SP_GetAllSortedProducts 8000,0,'productName','ASC',NULL, " + CurrentUserData.SupplierID + "", "AllProducts");

        }

        [HttpPost]

        public ActionResult DeleteProductDetail(int ID)
        {
            try
            {
                var _orderItems = db.OrderItems.Where(x => x.ProductID == ID).Count();
                if (_orderItems <= 0)
                {

                    var _obj = db.ProductAttributeWithQuantity.Where(x => x.Id == ID).FirstOrDefault();
                    _obj.IsActive = false;
                    db.SaveChanges();
                    return Json(new { Success = true, ex = "", data = "" });

                }
                return Json(new { Success = false, ex = "This item is currently in use", data = "" });

            }
            catch (Exception ex)
            {

                return Json(new { Success = false, ex = ex.Message.ToString(), data = "" });

            }
        }

        [HttpPost]

        public ActionResult DeleteProductImage(int ID)
        {
            try
            {

                _Imagesrepository.Delete(ID);
                _Imagesrepository.Save();

                return Json(new { Success = true, ex = "", data = "" });
            }
            catch (Exception ex)
            {

                return Json(new { Success = false, ex = ex.Message.ToString(), data = "" });

            }
        }
        public void DeleteOldAttributes(int productID)
        {
            var _db = new ApplicationDbContext();
            var _PrdouctData = _db.ProductAttributeWithQuantity.Where(x => x.ProductId == productID);
            if (_PrdouctData.Count() > 0)
            { _db.ProductAttributeWithQuantity.RemoveRange(_PrdouctData); _db.SaveChanges(); }
        }
        private string GetAttributeValues(List<ProductAttributeModelInner> list)
        {
            var _string = "";
            if (list.Count() > 0)
            {
                foreach (var _item in list)
                {
                    _string += _item.AttributeID.ToString() + "@@" + _item.Value + "##";
                }

            }
            return _string;
        }
    }
}