//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TesteCandidato.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class CEP
    {
        public int Id { get; set; }
        public string cep1 { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public Nullable<long> unidade { get; set; }
        public Nullable<int> ibge { get; set; }
        public string gia { get; set; }
    }
}