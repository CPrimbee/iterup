using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Api.Entities
{
    public class Resposta
    {
        public Resposta()
        {
            ValidarEtapa();
        }
        
        #region "Propriedades"
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumResposta { get; set; }

        [ForeignKey("FK_Etapa")]
        public int NumEtapa { get; set; }

        public string Legenda { get; set; }

        [Range(minimum: 1, maximum: (int) Int32.MaxValue)]
        public int NumProxEtapa { get; set; }
        #endregion

        #region "Navegação"
        public virtual Etapa FK_Etapa { get; set; }
        #endregion      

        private bool ValidarEtapa()
        {
            if (NumEtapa > 0)
                return true;
                
            return false;
        }

        private bool ValidarProxEtapa()
        {
            if (NumProxEtapa > 0)
                return true;
            
            return false;
        }
    }
}