//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClinicaOdontologica.Desfalcados.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Agendamento
    {
        public int ID_AGENDAMENTO { get; set; }
        [Required(ErrorMessage = "Este campo � obrigat�rio")]
        public System.DateTime DT_HORA_ATENDIMENTO { get; set; }
        public System.DateTime DT_INCLUSAO { get; set; }
        public Nullable<bool> FL_CANCELADO { get; set; }
        public string MOTIVO_CANCELAMENTO { get; set; }
        public Nullable<decimal> VL_ATENDIMENTO { get; set; }
        [Required(ErrorMessage = "Este campo � obrigat�rio")]
        public Nullable<int> ID_PACIENTE { get; set; }
        [Required(ErrorMessage = "Este campo � obrigat�rio")]
        public Nullable<int> ID_SERVICO { get; set; }
        [Required(ErrorMessage = "Este campo � obrigat�rio")]
        public Nullable<int> ID_FUNCIONARIO { get; set; }
    
        public virtual Funcionario Funcionario { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Servico Servico { get; set; }
    }
}
