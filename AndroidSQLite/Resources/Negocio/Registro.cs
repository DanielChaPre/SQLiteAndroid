using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidSQLite.Resources.Datos;
using AndroidSQLite.Resources.Modelo;

namespace AndroidSQLite.Resources.Negocio
{
    [Activity(Label = "Registro")]
    public class Registro:Activity
    {
        List<Persona> listaPersonas = new List<Persona>();
        BaseDatos baseDatos;
        public EditText edtNombre, edtEdad, edtOcupacion;
        public CheckBox chbMasculino, chbFemenino;
        public Button btnGuardar, btnCancelar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_registro);
            inicializar();
            accionarBotones();
        }
        public void inicializar()
        {
            baseDatos = new BaseDatos();
            baseDatos.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);
            edtNombre = FindViewById<EditText>(Resource.Id.edtNombre);
            edtOcupacion = FindViewById<EditText>(Resource.Id.edtOcupacion);
            edtEdad = FindViewById<EditText>(Resource.Id.edtEdad);
            chbMasculino = FindViewById<CheckBox>(Resource.Id.chbMasculino);
            chbFemenino = FindViewById<CheckBox>(Resource.Id.chbFemenino);
            btnGuardar = FindViewById<Button>(Resource.Id.btnGuardarRegistro);
            btnCancelar = FindViewById<Button>(Resource.Id.btnCancelarRegistro);
        }
        public void accionarBotones()
        {
            btnGuardar.Click += (s,e) =>
            {
                if (validarCamposVacios())
                {
                    if (validarEdad())
                    {
                        if (validarCheckBox())
                        {
                            guardarPersona();
                        }
                    }
                }   
            };
            btnCancelar.Click += delegate
            {
                regresarPantallaAnterior();
            };
        }
        public void guardarPersona()
        {
            Persona persona = new Persona()
            {
                nombre = edtNombre.Text,
                ocupacion = edtOcupacion.Text,
                edad = int.Parse(edtEdad.Text),
                sexo = checarCheckBox()
            };
            baseDatos.insertIntoTablePerson(persona);
            limpiarCampos();
            cargarDatos();
            regresarPantallaAnterior();
        }
       public void regresarPantallaAnterior()
        {
            var i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }
        public void cargarDatos()
        {
            listaPersonas = baseDatos.selectTablePerson();
            var adaptador = new AdaptadorListView(this, listaPersonas);
           // listaDatos.Adapter = adaptador;
        }
        
        public void limpiarCampos()
        {
            edtNombre.Text = "";
            edtEdad.Text = "";
            edtOcupacion.Text = "";
            chbMasculino.Checked = false;
            chbFemenino.Checked = false;
        }
        public string checarCheckBox()
        {
            string sexo = " ";
            if (chbMasculino.Checked)
            {
                sexo = "M";
            }
            else if (chbFemenino.Checked)
            {
                sexo = "F";
            }
            return sexo;
        }
        public Boolean validarCamposVacios()
        {
            if (edtNombre.Text.Equals("") || edtEdad.Text.Equals("") || edtOcupacion.Text.Equals(""))
            {
                Toast.MakeText(this, "Existen Campos Vacios", ToastLength.Long).Show();
                return false;
            }
            else
            {
                return true;
            }
        }
        public Boolean validarEdad()
        {
            int edad = int.Parse(edtEdad.Text);
            if (edad >= 100)
            {
                Toast.MakeText(this, "La edad es mayor a 100", ToastLength.Long).Show();
                Toast.MakeText(this, "No se puede registrar", ToastLength.Long).Show();
                return false;
            }
            else
            {
                return true;
            }
        }
        public Boolean validarCheckBox()
        {
            if (chbMasculino.Checked == false && chbFemenino.Checked == false)
            {
                Toast.MakeText(this, "Existen Campos Vacios", ToastLength.Long).Show();
                return false;
            }
            else if (chbMasculino.Checked == true && chbFemenino.Checked == true)
            {
                Toast.MakeText(this, "Ambos CheckBox estan seleccionados", ToastLength.Long).Show();
                Toast.MakeText(this, "Seleccione solamente uno", ToastLength.Long).Show();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}