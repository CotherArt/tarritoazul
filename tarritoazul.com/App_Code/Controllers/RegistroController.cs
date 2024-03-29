﻿using System;
using System.Configuration;
using System.Data.SqlClient;

internal class RegistroController
{
    public static readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TAConnectionString"].ConnectionString);

    public static Registro SelectById(int id_registro)
    {
        SqlCommand command = new SqlCommand("Select * from [REGISTROS] where id_registro=@idp", con);
        command.Parameters.AddWithValue("@idp", id_registro);
        try
        {
            con.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Registro r = new Registro();
                    r.Id_Registro = (int)reader["id_registro"];
                    r.Usuario = (string)reader["Usuario"];
                    r.Correo = (string)reader["Correo"];
                    r.Contrasena = (string)reader["Contrasena"];
                    con.Close();
                    return r;
                }
            }
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
        con.Close();
        return null;
    }

    public static Registro Insertar(Registro r) //insertar Registro a la BD y obtener el ID
    {
        //Definir la consulta
        string SQLInsert = String.Format("insert into REGISTROS( usuario, correo, contrasena) output INSERTED.id_registro " +
        "values('{0}','{1}','{2}');", r.Usuario, r.Correo, r.Contrasena);

        SqlCommand cmd = new SqlCommand(SQLInsert, con);

        try
        {
            //Abrir la coneccion con la BD
            con.Open();
            //Ejecutar la insercion y obtener el ID generado
            r.Id_Registro = (int)cmd.ExecuteScalar();
            //Cerrar la coneccion con la BD si se encuentra abierta
            con.Close();
            return r;
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
            return null;
        }
    }

    public static void Actualizar(Registro r)
    {
        //Definir la consulta
        string SQLUpdate = String.Format("update REGISTROS " +
            "set usuario='{0}', correo='{1}', contrasena='{2}'" +
            "where id_registro={3};", r.Usuario, r.Correo, r.Contrasena, r.Id_Registro);

        SqlCommand cmd = new SqlCommand(SQLUpdate, con);

        try
        {
            //Abrir la coneccion con la BD
            con.Open();
            //Ejecutar la instruccion
            cmd.ExecuteNonQuery();
            //Cerrar la coneccion con la BD si se encuentra abierta
            con.Close();
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void Eliminar(Registro r)
    {
        //Definir la consulta
        string SQLDelete = String.Format("delete from REGISTROS where id_registro = {0};", r.Id_Registro);

        SqlCommand cmd = new SqlCommand(SQLDelete, con);

        try
        {
            //Abrir la coneccion con la BD
            con.Open();
            //Ejecutar la instruccion
            cmd.ExecuteNonQuery();
            //Cerrar la coneccion con la BD si se encuentra abierta
            con.Close();
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }
}