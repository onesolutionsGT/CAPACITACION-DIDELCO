using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAPbobsCOM;

namespace DidelcoCapacitacionDiApi
{
    public partial class Form1 : Form
    {
        //objeto Company para conexion a SAP 
        Company oCompany = new Company();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
            oCompany.Connect();

            oCompany.StartTransaction();
            try
            {
                //Agregar registo a campo de usuario.
                //Factura.UserFields.Fields.Item("U_TelefonoAbuelita").Value = 123123123;

                //string telefono = Factura.UserFields.Fields.Item("U_TelefonoAbuelita").Value;

                // creacion de un socio de negocios

                //Documentos
                Documents Factura = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);

                //Crea Documento de pedido

                Documents Pedido = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);

                Pedido.DocDate = DateTime.Now;
                Pedido.DocDueDate = DateTime.Now;
                Pedido.CardCode = "";
                Pedido.Add();


                //Documents NotaCredito = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oCreditNotes);
                //BusinessPartners
                //BusinessPartners Socio = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                //Items
                //Items Articulo = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);

                //Servicio en company
                //CompanyService servicio = oCompany.GetCompanyService;


                //Encabezado de documento
                Factura.DocDate = DateTime.Now;
                Factura.DocDueDate = DateTime.Now;
                Factura.CardCode = "C00001";
                Factura.CardName = "Nomrbe de usuario";
                Factura.UserFields.Fields.Item("U_CAMPOUSUARIO").Value = 10;
                Factura.UserFields.Fields.Item("U_CAMPO2").Value = "jooa";

                //Lineas del documento
                for (int i = 0; i <= 5; i++)
                {
                    Factura.Lines.ItemCode = "Art1029";
                    Factura.Lines.ItemDescription = "Articulo numero 1029";
                    Factura.Lines.TaxCode = "IVA";
                    Factura.Lines.LineTotal = 1000.00;
                    Factura.Lines.UserFields.Fields.Item("U_TIPOVENTA").Value = "S";
                    Factura.Lines.Add();
                }
                Factura.DocTotal = 1000.00;

                int respuesta = Factura.Add();

                if (respuesta != 0)
                {
                    //retorna un error cuando no se genera la factura en sap
                    int codigoError = oCompany.GetLastErrorCode();
                    string descripcion = oCompany.GetLastErrorDescription();
                    throw new Exception(" Invoice; Error codigo: " + codigoError + " Descripcion: " + descripcion);
                }

                //Continua el codigo luego de ingreso de factura exitoso.
                oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
            }
            catch (Exception ex)
            {
                oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                MessageBox.Show(ex.Message);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            // ID De cada objeto.
            // Documentos "DocEntry" - OINV
            // BusinessParntners "CardCode" - OCRD
            // Items "ItemCode" - OITM


            Company oCompany = new Company();
            oCompany.Connect();

            Documents factura = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);

            //Creacion de query con RecordSet para obtener el docentry de una factura:
            int docentry = 100;

            //Obtiene un objeto por medio de su ID
            if (factura.GetByKey(docentry) == false)
            {
                throw new Exception("No se encontro la factura");
            }

            int docnum = factura.DocNum;
            string campo = factura.UserFields.Fields.Item("U_CAMPO").Value;

            //P  - Pendiente
            //R - Rechazado
            //A - Autorizado

            string campoCatalogoEstadoFel = factura.UserFields.Fields.Item("U_ESTADO_FEL").Value;

            string descripcionCatalogoEstadoFel = obtenerDescripcion(campoCatalogoEstadoFel, "OINV", "U_ESTADO_FEL", oCompany);

            factura.CardName = docnum + "-" + campo;

            int estado = factura.Update();



            if (estado != 0)
            {
                //retorna un error cuando no se actualiza la factura en sap
                int codigoError = oCompany.GetLastErrorCode();
                string descripcion = oCompany.GetLastErrorDescription();
                throw new Exception(" Invoice; Error codigo: " + codigoError + " Descripcion: " + descripcion);
            }

            Marshal.ReleaseComObject(factura);
            factura = null;
            GC.Collect();

            //Equivalente de codigo en HANA SQL (NUNCA ACTUALIZAR CAMPOS NATIVOS DE SAP DE ESTA MANERA) SOLO DI API
            //string a = @"UPDATE OINV (""CardName"") Values(""DocNum"" || '-' || ""U_CAMPO"") where ""DocEntry"" = 100";
            return;
        }

        private string obtenerDescripcion(string campoCatalogoEstadoFel, string tabla, string campo, Company oCompany)
        {
            string query = @"select distinct b.""Descr""  from CUFD a  
                                inner join UFD1 b ON a.""FieldID"" = b.""FieldID"" 
                                where b.""FldValue"" = 'INVALIDAR' and a.""AliasID"" = 'ESTADO_FEL'
                                and a.""TableID"" = 'OINV'";
            Recordset rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            rs.DoQuery(query);
            string resp = rs.Fields.Item(0).ToString();

            Marshal.ReleaseComObject(rs);
            rs = null;
            GC.Collect();

            return resp;
        }

        private void ConexionACompany()
        {
            //direccion del servidor de base de datos
            string servidor = "NBD@129.23.123.4:30013";
            oCompany.Server = servidor;

            //nombre de usuario de sap (manager)
            string usersap = "manager";
            oCompany.UserName = usersap;

            //contraseña del usuario de sap (123123)
            string passwordsap = "123123";
            oCompany.Password = passwordsap;

            //nombre de la base de datos
            string sociedad = "SBO_sociedad";
            oCompany.CompanyDB = sociedad;

            //usuario de la base de datos
            string Db_user = "USERSAP";
            oCompany.DbUserName = Db_user;

            //contraseña de la base de datos
            string Db_password = "1235430";
            oCompany.DbPassword = Db_password;

            //(opcional) - conexion mediante windows user 
            //oCompany.UseTrusted = true;

            //tipo de servidor de base de datos
            BoDataServerTypes DB_Type = (BoDataServerTypes)10;
            oCompany.DbServerType = DB_Type;

            //conexion exitosa = 0
            int respuestaDeConexion = oCompany.Connect();

            if (respuestaDeConexion != 0)
            {
                int codigoError = oCompany.GetLastErrorCode();
                string descripcionError = oCompany.GetLastErrorDescription();
                throw new Exception($"Error[Codigo:{codigoError}; Descripcion:{descripcionError}]");
            }

            bool conectado = oCompany.Connected;

            if (conectado == true)
            {
                Console.WriteLine("Conectado a la base");
            }

            Console.WriteLine("Conexion exitosa");

            Desconectar();

        }

        public void CompanyServiceMethod()
        {
            try
            {
                CompanyService CompanyService = oCompany.GetCompanyService();
                SeriesService SeriesService = CompanyService.GetBusinessService(ServiceTypes.SeriesService);
                Series oNewSeries = SeriesService.GetDataInterface(SeriesServiceDataInterfaces.ssdiSeries);

                oNewSeries.Name = "CCFE";
                oNewSeries.InitialNumber = 1000000;
                oNewSeries.LastNumber = 1999999;
                oNewSeries.Prefix = "";
                oNewSeries.Suffix = "";
                oNewSeries.Remarks = "";
                oNewSeries.GroupCode = BoSeriesGroupEnum.sg_Group1;
                oNewSeries.Document = "13";
                oNewSeries.DocumentSubType = "";
                oNewSeries.SeriesType = BoSeriesTypeEnum.stDocument;
                oNewSeries.PeriodIndicator = "1";

                SeriesService.AddSeries(oNewSeries);
                Console.WriteLine("Agregada serie: " + oNewSeries.Name);
                Marshal.ReleaseComObject(SeriesService);
                Marshal.ReleaseComObject(oNewSeries);
                GC.Collect();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (oCompany != null)
            {
                if (oCompany.Connected == true)
                {
                    ErrorLabel.Text = "YA CONECTADO";
                    return;
                }
                oCompany = null;
            }
            ErrorLabel.Text = "...";
            oCompany = new Company();

            oCompany.Server = ServerTxt.Text;
            oCompany.UserName = UserNameTxt.Text;
            oCompany.Password = PasswordTxt.Text;
            oCompany.CompanyDB = CompanyDbTxt.Text;
            oCompany.DbUserName = UserDbTxt.Text;
            oCompany.DbPassword = PassDbTxt.Text;
            //oCompany.DbServerType = (BoDataServerTypes) Convert.ToInt32(DbType.Text);
            switch (DbType.Text)
            {
                case "dst_MSSQL": oCompany.DbServerType = BoDataServerTypes.dst_MSSQL; break;
                case "dst_DB_2": oCompany.DbServerType = BoDataServerTypes.dst_DB_2; break;
                case "dst_SYBASE": oCompany.DbServerType = BoDataServerTypes.dst_SYBASE; break;
                case "dst_MSSQL2005": oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2005; break;
                case "dst_MAXDB": oCompany.DbServerType = BoDataServerTypes.dst_MAXDB; break;
                case "dst_MSSQL2008": oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2008; break;
                case "dst_MSSQL2012": oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2012; break;
                case "dst_MSSQL2014": oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2014; break;
                case "dst_HANADB": oCompany.DbServerType = BoDataServerTypes.dst_HANADB; break;
                case "dst_MSSQL2016": oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2016; break;
                case "dst_MSSQL2017": oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2017; break;
                case "dst_MSSQL2019": oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2019; break;
                default: break;
            }

            int respuesta = oCompany.Connect();
            if (respuesta != 0)
            {
                string error = GetStringError(oCompany);
                ErrorLabel.Text = error;
            }
            if (oCompany.Connected == true)
            {
                ErrorLabel.Text = "CONECTADO";
            }
        }


        public string GetStringError(Company oCompany)
        {
            int codigo = oCompany.GetLastErrorCode();
            string mensaje = oCompany.GetLastErrorDescription();
            string respuesta = $"ERROR[Codigo:{codigo} Descripcion:{mensaje}]";
            return respuesta;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool desconectado = Desconectar();

            if (desconectado == false)
            {
                ErrorLabel.Text = "No se pudo desconectar de la sociedad...";
            }
            else
            {
                ErrorLabel.Text = "DESCONECTADO";
            }
        }

        private bool Desconectar()
        {
            try
            {
                if (oCompany != null)
                {
                    if (oCompany.Connected == true)
                    {
                        oCompany.Disconnect();
                        Marshal.ReleaseComObject(oCompany);
                        GC.Collect();
                        oCompany = null;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void Form_exit(object sender, FormClosedEventArgs e)
        {
            Desconectar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Conectado())
            {
                CrearSocio();
            }
            else
            {
                ErrorLabel.Text = "Company no se encuentra en estado conectado";
            }
        }

        private bool Conectado()
        {
            if (oCompany != null)
            {
                if (oCompany.Connected == true)
                {
                    return true;
                }
            }
            return false;
        }

        private void CrearSocio()
        {
            BusinessPartners oBusinessPartner = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
            //Documents oOrder = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
            //oOrder.Lines.ItemCode = "";
            //oOrder.Lines.Add();
            //oOrder.Add();

            oBusinessPartner.CardCode = cardcodeTxt.Text;
            oBusinessPartner.CardName = nombreTxt.Text;
            oBusinessPartner.FederalTaxID = "00000000000000";
            oBusinessPartner.Series = 125; // CLIENTE Series

            //group name
            //oBusinessPartner.Properties[4] = BoYesNoEnum.tYES;
            //oBusinessPartner.Properties[5] = BoYesNoEnum.tYES;
            //oBusinessPartner.EmailAddress = "correo@correo.com";
            //oBusinessPartner.Phone1 = "12312323";
            //oBusinessPartner.Addresses.AddressType = BoAddressType.bo_ShipTo;
            //oBusinessPartner.Addresses.AddressName = "address1";
            //oBusinessPartner.Addresses.AddressName2 = "address2";
            //oBusinessPartner.Addresses.Add();
            //NUEVOS DATOS DIDELCO
            //oBusinessPartner.CardName = "nombre";
            oBusinessPartner.UserFields.Fields.Item("U_Cod_Doc").Value = "string"; // 13,2,3,36,37; 36 = nit                                                                                                 //Doc_Identidad
            //oBusinessPartner.UserFields.Fields.Item("U_DUI").Value = "string"; // Documento de identidad                                                                       //Tipo_Documento
            //oBusinessPartner.UserFields.Fields.Item("U_FacSerie").Value = "string"; // ANTICIPO, CCF, CDO, CLI, COTIZACION, EXC, EXO_PLAN, etc...                                                                         //Correo electronico
            //oBusinessPartner.EmailAddress = "correo";
            //oBusinessPartner.Phone1 = "asdasd";
            //oBusinessPartner.Properties[4] = BoYesNoEnum.tYES; // 4, 5, 6, 7, 8, 9; 9 = otros                                                                  //Condicion de pago
            //oBusinessPartner.PayTermsGrpCode = 5; // 1 = contado; -1 = 30 Dias                                                                                //Categoria descuento
            //oBusinessPartner.UserFields.Fields.Item("U_clsdes").Value = "9"; // 1,2,3,4,5,6,7,8,9; 9 = ecommerce

            switch (tipoTxt.Text)
            {
                case "CLIENTE":
                    oBusinessPartner.CardType = BoCardTypes.cCustomer;
                    break;
                case "PROVEEDOR":
                    oBusinessPartner.CardType = BoCardTypes.cSupplier;
                    break;
                case "LEAD":
                    oBusinessPartner.CardType = BoCardTypes.cLid;
                    break;
            }

            int respuesta = oBusinessPartner.Add();
            if (respuesta != 0)
            {
                string erroes = GetStringError(oCompany);
                ErrorLabel.Text = erroes;
                return;
            }
            oCompany.GetNewObjectCode(out string id_cardcode_creado);
            ErrorLabel.Text = "CREADO CON EL CODIGO: " + id_cardcode_creado;
        }


        //testing
        private void testing()
        {
            UserObjectsMD oUserObjectMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);

            string numero = "10000";
            string query = $@"SELECT ""DocEntry"" from OINV where ""DocNum"" = {numero} AND 1 = 1";
            var s = GetSingleData(query);



        }


        private dynamic GetSingleData(string query)
        {
            //INSERT Solo a tablas definidas por el usuario que no sean parte de un UDO.
            //SELECT Permitidos
            //UPDATE Solo a campos de usuario
            //DELETE Prohibido
            try
            {
                Recordset Record = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                Record.DoQuery(query);
                Record.MoveFirst();

                if (Record.Fields.Count > 1)
                {
                    return "Esta consulta trae mas de una columna";
                }

                string valor = Record.Fields.Item(0).Value + "";

                Marshal.ReleaseComObject(Record);
                GC.Collect();
                Record = null;

                return valor;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = queryTxt.Text;
            console.Text = console.Text + $"\n------Ejecutando query: {query}";

            var dato = GetSingleData(query);

            console.Text = console.Text + $"\n------El query se ejecuto con exito";
            console.Text = console.Text + $"------Resultado col 1, reg 1: {dato}";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string query = queryTxt.Text;
            console.Text = console.Text + $"\r\n----Ejecutando query: {query}";

            Recordset rs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            rs.DoQuery(query);

            rs.MoveFirst();

            while (!rs.EoF)
            {
                string linea = "|";
                for (int i = 0; i < rs.Fields.Count; i++)
                {
                    linea = linea + "|" + rs.Fields.Item(i).Value + "|";
                }
                linea = linea + "|";
                console.Text = console.Text + linea + "\r\n";
                rs.MoveNext();
            }

            Marshal.ReleaseComObject(rs);
            GC.Collect();
            rs = null;
            //UPDATE OINV SET "U_FIRMA_ELETRONICA" = 'Test de di api' WHERE "DocEntry" = '23081'
            //Consulta con mas datos
            //SELECT TOP 10 "DocEntry", "DocNum", "CardCode" FROM OINV ORDER BY "DocEntry" desc
        }

        //Metodo para crear una tabla definida por el usuario.
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {

                UserTablesMD oUserTableMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables);
                string name = TableNameTxt.Text;
                string descripcion = TableDescTxt.Text;

                if (oUserTableMD.GetByKey(name) == true)
                {
                    console.Text = "Tabla ya existe. imposible crear de nuevo";

                    Marshal.ReleaseComObject(oUserTableMD);
                    GC.Collect();
                    oUserTableMD = null;

                    return;
                }



                //obviar
                oUserTableMD.DisplayMenu = BoYesNoEnum.tYES;

                oUserTableMD.TableDescription = descripcion;
                oUserTableMD.TableName = name;
                oUserTableMD.TableType = BoUTBTableType.bott_NoObject;

                int resp = oUserTableMD.Add();

                if (resp != 0)
                {
                    console.Text = GetStringError(oCompany);
                    Marshal.ReleaseComObject(oUserTableMD);
                    GC.Collect();
                    oUserTableMD = null;
                    return;
                }

                oCompany.GetNewObjectCode(out string codigoNuevo);

                console.Text = $"Tabla {codigoNuevo} creada con exito";

                Marshal.ReleaseComObject(oUserTableMD);
                GC.Collect();
                oUserTableMD = null;

            }
            catch (Exception ex)
            {
                console.Text = ex.Message;
            }
        }




        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            UserFieldsMD oUserFieldsMD = oCompany.GetBusinessObject(BoObjectTypes.oUserFields);
            string nombreTabla = "@" + TableNameTxt.Text;

            //Tabla CUFD

            //Select "FieldID" from OINV where "AliasID" = 'CAMPOTEST' and TableID = '@TBL01'

            // 102 no es el field ID, es un vaor quemado para ejemplo.
            //if (oUserFieldsMD.GetByKey(nombreTabla, 102) == true)
            //{
            //    return;
            //}

            oUserFieldsMD.TableName = nombreTabla;
            oUserFieldsMD.Name = "CAMPOTEST2";
            oUserFieldsMD.Description = "campo de prueba";
            oUserFieldsMD.Size = 254;
            oUserFieldsMD.Type = BoFieldTypes.db_Alpha;
            oUserFieldsMD.SubType = BoFldSubTypes.st_None;
            oUserFieldsMD.Mandatory = BoYesNoEnum.tNO;

            int resp = oUserFieldsMD.Add();

            if (resp != 0)
            {
                console.Text = GetStringError(oCompany);
                Marshal.ReleaseComObject(oUserFieldsMD);
                GC.Collect();
                oUserFieldsMD = null;
                return;
            }
            oCompany.GetNewObjectCode(out string Codigo);
            console.Text = $"Campo creado con exito: {Codigo}";
            Marshal.ReleaseComObject(oUserFieldsMD);
            GC.Collect();
            oUserFieldsMD = null;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string tableName = TableNameTblTxt.Text;
                string code = CodeTblTxt.Text;
                string name = NameTblTxt.Text;
                string UF1 = UFTblTxt.Text;
                string UF2 = UF2TblTxt.Text;

                UserTable oUserTable = oCompany.UserTables.Item(tableName);

                oUserTable.Code = code;
                oUserTable.Name = name;
                oUserTable.UserFields.Fields.Item("U_CAMPOTEST").Value = UF1;
                oUserTable.UserFields.Fields.Item("U_CAMPOTEST2").Value = UF2;

                int respuesta = oUserTable.Add();

                if(respuesta != 0)
                {
                    consoleTBL.Text = consoleTBL.Text + GetStringError(oCompany) + "\r\n";
                    Marshal.ReleaseComObject(oUserTable);
                    GC.Collect();
                    oUserTable = null;
                    return;
                }

                consoleTBL.Text = consoleTBL.Text + "Registro ingresado con exito." + "\r\n";
                Marshal.ReleaseComObject(oUserTable);
                GC.Collect();
                oUserTable = null;
            }
            catch (Exception ex)
            {
                consoleTBL.Text = consoleTBL.Text + ex.Message + "\r\n";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string UdoCode = "UDO_PACHON";
                string UdoName = "Udo para pachon";
                string UdoMenuUID = "UDO_PACHON";
                string UdoMenuCaption = "udo de pachones";
                
                string TableEnca = UdoEncaTxt.Text;
                string TableDeta = UdoDetaTxt.Text;

                //UDO_PACHON MasterData
                //UDO_PACHON_DET MasterDataLines

                UserObjectsMD oUDO = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD);

                if (oUDO.GetByKey(UdoCode) == true)
                {
                    consoleUdo.Text = consoleUdo.Text + $"El udo {UdoCode} ya existe \r\n "; 
                    Marshal.ReleaseComObject(oUDO);
                    GC.Collect();
                    oUDO = null;
                    return;
                }


                //Datos Generales
                oUDO.Code = UdoCode;
                oUDO.Name = UdoName;
                oUDO.ObjectType = BoUDOObjType.boud_MasterData;
                oUDO.ManageSeries = BoYesNoEnum.tYES;
                //Tabla maestra
                oUDO.TableName = TableEnca;
                //campos por defecto
                //--Code
                //--Name

                //Menu visual
                oUDO.MenuItem = SAPbobsCOM.BoYesNoEnum.tYES;
                oUDO.MenuUID = UdoMenuUID;
                oUDO.MenuCaption = UdoMenuCaption;
                oUDO.FatherMenuID = 43679;
                oUDO.Position = 1;
                //CheckBox propiedades CAN
                oUDO.CanApprove = BoYesNoEnum.tYES;
                oUDO.CanArchive = BoYesNoEnum.tNO;
                oUDO.CanCancel = BoYesNoEnum.tYES;
                oUDO.CanClose = BoYesNoEnum.tYES;
                oUDO.CanCreateDefaultForm = BoYesNoEnum.tYES;
                oUDO.CanDelete = BoYesNoEnum.tYES;
                oUDO.CanFind = BoYesNoEnum.tYES;
                oUDO.CanLog = BoYesNoEnum.tYES;
                oUDO.CanYearTransfer = BoYesNoEnum.tNO;

                //FormColumns
                oUDO.FormColumns.FormColumnAlias = "Code";
                oUDO.FormColumns.FormColumnDescription = "codigo";
                oUDO.FormColumns.Editable = BoYesNoEnum.tNO;
                oUDO.FormColumns.Add();
                oUDO.FormColumns.FormColumnAlias = "DocEntry";
                oUDO.FormColumns.FormColumnDescription = "ID documento";
                oUDO.FormColumns.Editable = BoYesNoEnum.tNO;
                oUDO.FormColumns.Add();
                oUDO.FormColumns.FormColumnAlias = "CreateDate";
                oUDO.FormColumns.FormColumnDescription = "Fecha de creacion";
                oUDO.FormColumns.Editable = BoYesNoEnum.tNO;
                oUDO.FormColumns.Add();
                //DataSource nos indica de donde esta ingresando el registro
                // I interface
                // U Usario
                // O Sap
                oUDO.FormColumns.FormColumnAlias = "DataSource";
                oUDO.FormColumns.FormColumnDescription = "Fuente de datos";
                oUDO.FormColumns.Editable = BoYesNoEnum.tNO;
                oUDO.FormColumns.Add();
                oUDO.FormColumns.FormColumnAlias = "U_VALOR";
                oUDO.FormColumns.FormColumnDescription = "campo de usuario";
                oUDO.FormColumns.Editable = BoYesNoEnum.tYES;
                oUDO.FormColumns.SonNumber = 0;
                oUDO.FormColumns.Add();

                //FindColumns
                oUDO.FindColumns.ColumnAlias = "Name";
                oUDO.FindColumns.ColumnDescription = "Nombre";
                oUDO.FindColumns.Add();
                oUDO.FindColumns.ColumnAlias = "DocEntry";
                oUDO.FindColumns.ColumnDescription = "ID documento";
                oUDO.FindColumns.Add();
                oUDO.FindColumns.ColumnAlias = "U_VALOR";
                oUDO.FindColumns.ColumnDescription = "campo de usuario";
                oUDO.FindColumns.Add();

                //ChildTables
                oUDO.ChildTables.TableName = TableDeta;
                oUDO.ChildTables.Add();

                //EnhancedFormColumns
                oUDO.EnhancedFormColumns.ColumnAlias = "Code";
                oUDO.EnhancedFormColumns.ColumnDescription = "Codigo";
                oUDO.EnhancedFormColumns.ColumnIsUsed = BoYesNoEnum.tYES;
                oUDO.EnhancedFormColumns.Editable = BoYesNoEnum.tYES;
                oUDO.EnhancedFormColumns.ChildNumber = 0;
                oUDO.EnhancedFormColumns.Add();
                oUDO.EnhancedFormColumns.ColumnAlias = "U_VALOR";
                oUDO.EnhancedFormColumns.ColumnDescription = "valor";
                oUDO.EnhancedFormColumns.ColumnIsUsed = BoYesNoEnum.tYES;
                oUDO.EnhancedFormColumns.Editable = BoYesNoEnum.tYES;
                oUDO.EnhancedFormColumns.ChildNumber = 1;
                oUDO.EnhancedFormColumns.Add();

                int respuesta = oUDO.Add();
                if(respuesta != 0)
                {
                    Marshal.ReleaseComObject(oUDO);
                    GC.Collect();
                    oUDO = null;
                    consoleUdo.Text = consoleUdo.Text + GetStringError(oCompany) + "\r\n";
                    return;
                }
                Marshal.ReleaseComObject(oUDO);
                GC.Collect();
                oUDO = null;
                consoleUdo.Text = consoleUdo.Text + "Creado con exito el UDO" + "\r\n";

            }
            catch (Exception ex)
            {
                consoleUdo.Text = consoleUdo.Text + ex.Message + "\r\n";
            }
        }
    }
}


