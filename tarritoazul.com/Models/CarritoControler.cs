﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tarritoazul.com.Models
{
    public class CarritoControler
    {
        //Agregar un producto al carrito de la Session
        public static void AddProducto(Producto p)
        {
            List<Producto> lista = new List<Producto>();
            //Si no hay carrito, crear uno
            if (HttpContext.Current.Session["carrito"] == null)
            {
                HttpContext.Current.Session["carrito"] = lista;
            }
            else
            {
                lista = (List<Producto>)HttpContext.Current.Session["carrito"];
            }

            lista.Add(p);
            HttpContext.Current.Session["carrito"] = lista;
        }
    }
}