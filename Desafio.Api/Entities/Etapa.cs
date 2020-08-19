using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Desafio.Api.Enums;

namespace Desafio.Api.Entities
{
    public class Etapa
    {
        public Etapa()
        {
            this.FK_Respostas = new List<Resposta>();
        }

        #region "Propriedades"

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumEtapa { get; set; }

        public string TextoEtapa { get; set; }

        [Range(minimum: 1, maximum: (int) Int32.MaxValue)]
        public int? NumProxEtapa { get; set; }

        public ETipoEtapa? TipoEtapa { get; set; }
        #endregion

        #region "Navegação"
        public virtual ICollection<Resposta> FK_Respostas { get; set; }
        #endregion
    }
}