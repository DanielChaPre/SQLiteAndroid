using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using AndroidSQLite.Resources.Modelo;
using System.Collections.Generic;
using AndroidSQLite.Resources.Datos;
using AndroidSQLite.Resources;
using Android.Util;
using AndroidSQLite.Resources.Negocio;
using Android.Content;

namespace AndroidSQLite
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ListView listaDatos;
        List<Persona> listaPersonas = new List<Persona>();
        BaseDatos  baseDatos;
       
        public Button btnIrRegistro;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            inicializar();
            //Cargar Datos
              cargarDatos();
              //Acciones Botones
              accionarBotones();
              seleccionaritemLista();
        }
        public void inicializar()
        {
            baseDatos = new BaseDatos();
            baseDatos.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);
            listaDatos = FindViewById<ListView>(Resource.Id.listView1);
            btnIrRegistro = FindViewById<Button>(Resource.Id.btnIrRegistro);
        }
        public void accionarBotones()
        {
            btnIrRegistro.Click += (sender, args) =>
            {
                var i = new Intent(this, typeof(Registro));
                StartActivity(i);
            };
        }
        public void seleccionaritemLista()
        {
            listaDatos.ItemClick += (s, e) => {
                var txtNombre = e.View.FindViewById<TextView>(Resource.Id.textView1);
                var txtEdad = e.View.FindViewById<TextView>(Resource.Id.textView2);
                var txtOcupacion = e.View.FindViewById<TextView>(Resource.Id.textView3);
                var txtSexo = e.View.FindViewById<TextView>(Resource.Id.textView4);
                /*edtNombre.Text = txtNombre.Text;
                edtNombre.Tag = e.Id;
                edtEdad.Text = txtEdad.Text.Substring(0, 2);
                edtOcupacion.Text = txtOcupacion.Text;
                seleccionarCheckbox(txtSexo.Text);*/
                //string id = txtNombre.Text;
                irAcciones( txtNombre.Text, txtEdad.Text.Substring(0,2), txtOcupacion.Text, txtSexo.Text);
            };
        }
        public void cargarDatos()
        {
            listaPersonas = baseDatos.selectTablePerson();
            var adaptador = new AdaptadorListView(this, listaPersonas);
            listaDatos.Adapter = adaptador;
            validarListaVacia();
        }
        public Boolean validarListaVacia()
        {
            if (listaDatos.Count == 0)
            {
                Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show();
                Toast.MakeText(this, "Registre a una persona", ToastLength.Long).Show();
                return false;
            }
            else
            {
                return true;
            }
        }
        public void irRegistro()
        {
            var i = new Intent(this, typeof(Registro));
            StartActivity(i);
        }
        public void irAcciones( string nombre,string edad, string ocupacion, string sexo)
        {
            Intent intent = new Intent(this, typeof(Acciones));
           // intent.PutExtra(Acciones.ID, id);
            intent.PutExtra(Acciones.NOMBRE, nombre);
            intent.PutExtra(Acciones.EDAD, edad);
            intent.PutExtra(Acciones.OCUPACION, ocupacion);
            intent.PutExtra(Acciones.SEXO, sexo);
            StartActivity(intent);
        }
    }
}