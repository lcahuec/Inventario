//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventarioMedicina.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inventario
    {
        public int Id_Inventario { get; set; }
        public int Producto { get; set; }
        public int Cant_Inicial { get; set; }
        public int Cantildad_Dispo { get; set; }
    
        public virtual TBLPRODUCTO TBLPRODUCTO { get; set; }
    }
}