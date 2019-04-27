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
using SQLite;

namespace AndroidSQLite.Resources.Modelo
{
    public class Persona
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string nombre { get; set; }
        public int edad { get; set; }
        public string ocupacion { get; set; }
        public string sexo { get; set; }

    }
}