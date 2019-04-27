using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidSQLite.Resources.Modelo;
using Java.Lang;

namespace AndroidSQLite.Resources.Negocio
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtNombre { get; set; }
        public TextView txtEdad { get; set; }
        public TextView txtOcupacion { get; set; }
        public TextView txtSexo { get; set; }
    }
    public class AdaptadorListView: BaseAdapter
    {
        private Activity actividad;
        private List<Persona> listaPersonas;
        public AdaptadorListView(Activity actividad, List<Persona> listaPersona)
        {
            this.actividad = actividad;
            this.listaPersonas = listaPersona;
        }

        public override int Count
        {
            get
            {
                return listaPersonas.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return listaPersonas[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var vista = convertView ?? actividad.LayoutInflater.Inflate(Resource.Layout.listaAdaptable,parent, false);

            var txtNombre = vista.FindViewById<TextView>(Resource.Id.textView1);
            var txtEdad = vista.FindViewById<TextView>(Resource.Id.textView2);
            var txtOcupacion = vista.FindViewById<TextView>(Resource.Id.textView3);
            var txtSexo = vista.FindViewById<TextView>(Resource.Id.textView4);

            txtNombre.Text = listaPersonas[position].nombre;
            txtEdad.Text =""+listaPersonas[position].edad+" años";
            txtOcupacion.Text = listaPersonas[position].ocupacion;
            txtSexo.Text =listaPersonas[position].sexo;

            return vista;
        }
    }
}