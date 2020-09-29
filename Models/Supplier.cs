using System;
using System.Collections.Generic;
using System.Text;

namespace LenguajeProgramacionII.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Representant { get; set; }
        public string RNC { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public bool IsProvider { get; set; }
        public string RPE { get; set; }
    }
}
