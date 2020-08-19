using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Desafio.Api.Entities;
using Desafio.Api.Enums;

namespace Desafio.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(DesafioContext context)
        {
            if (!context.Etapas.Any())
            {
                context.Etapas.AddRange(
                    new Etapa
                    {
                        TextoEtapa = "Bom dia, como vai",
                        NumProxEtapa = 2, 
                        TipoEtapa = ETipoEtapa.MENSAGEM
                    },
                    new Etapa
                    {
                        TextoEtapa = "Me diga, você já utilizou os nossos produtos?", 
                        TipoEtapa = ETipoEtapa.PERGUNTA
                    },
                    new Etapa
                    {
                        TextoEtapa = "Que bacana, tomara que vc tenha gostado", 
                        NumProxEtapa = 1, 
                        TipoEtapa = ETipoEtapa.MENSAGEM
                    },
                    new Etapa
                    {
                        TextoEtapa = "Não? Então deixa eu te apresentar?", 
                        TipoEtapa = ETipoEtapa.PERGUNTA
                    },
                    new Etapa
                    {
                        TextoEtapa = "Nossos produtos são os mais TOPs das Galáxias!", 
                        NumProxEtapa = 1, 
                        TipoEtapa = ETipoEtapa.MENSAGEM
                    }
                );   

                context.SaveChanges();
            }

            if (!context.Respostas.Any())
            {
                context.Respostas.AddRange(
                    new Resposta
                    {
                        NumEtapa = 2,
                        Legenda = "Sim", 
                        NumProxEtapa = 3
                    },
                    new Resposta
                    {
                        NumEtapa = 2,
                        Legenda = "Não", 
                        NumProxEtapa = 4
                    },
                    new Resposta
                    {
                        NumEtapa = 4,
                        Legenda = "Sim", 
                        NumProxEtapa = 5
                    },
                    new Resposta
                    {
                        NumEtapa = 4,
                        Legenda = "Não", 
                        NumProxEtapa = 1
                    }
                );

                context.SaveChanges();
            }
        }
    }
}