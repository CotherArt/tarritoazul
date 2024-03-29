﻿using System;
using System.Web.UI;

namespace tarritoazul.com.forms
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        private static Producto producto;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Revisar si la url contiene el parametro id
                if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    producto = ProductoController.SelectById(id);
                    SetValues(producto);
                }
            }
        }

        //Pasar los atributos del producto al formulario
        private void SetValues(Producto p)
        {
            lbNombre.Text = p.Nombre;
            lbPrecio.Text = "$" + p.Precio.ToString();
            lbDescripcion.Text = p.Descripcion;

            string img = ProductoController.GetProductMedia(p.Id_Producto);
            string imgurl = "~/imgs/producto/";
            if (img != "")
            {
                imgurl += img;
            }
            else
            {
                imgurl += "placeholder.jpg";
            }
            Log(img);
            imgProducto.ImageUrl = imgurl;
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            //Agrega el producto al Session["carrito"]
            CarritoController.AddProducto(producto);
            //Recarga la pagina para que se actualice
            Response.Redirect("~/default.aspx");
        }

        public void Log(string msg)
        {
            Page.Response.Write("<script>console.log('" + msg + "');</script>");
        }
    }
}