using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromotorDePeliculas.Models
{
    public class MaxPalabras : ValidationAttribute
    {
        private readonly int máximo;
        public MaxPalabras(int m) : base("{0} Tiene muchas palabras")
        {
            máximo = m;
        }
        protected override ValidationResult IsValid(object valorPropiedad, ValidationContext ctx)
        {
            if (valorPropiedad != null)
            {
                var propiedadString = valorPropiedad.ToString();
                if (propiedadString.Split(' ').Length > máximo)
                {
                    var mensaje = FormatErrorMessage(ctx.DisplayName);
                    return new ValidationResult(mensaje);
                }
            }
            return ValidationResult.Success;
        }
    }

    public class SoloLetras : ValidationAttribute
    {


        public SoloLetras() : base("{0} Los nombre no pueden tener números")

        {

        }

        protected override ValidationResult IsValid(object valorPropiedad, ValidationContext ctx)

        {
            if (valorPropiedad != null)
            {
                var cadena = valorPropiedad.ToString();
                Boolean encontrado = false;
                char[] caracteres = cadena.ToCharArray();

                for (int i = 0; i < cadena.Length && !encontrado; i++)
                {

                    if (caracteres[i] == '0' || caracteres[i] == '1' || caracteres[i] == '2' || caracteres[i] == '3' || caracteres[i] == '4' || caracteres[i] == '5' ||
                        caracteres[i] == '7' || caracteres[i] == '8' || caracteres[i] == '9' || caracteres[i] == '6')
                    {
                        encontrado = true;
                        var mensaje = FormatErrorMessage(ctx.DisplayName);
                        return new ValidationResult(mensaje);
                    }
                }
            }

            return ValidationResult.Success;
        }
    }

    public class SoloNumeros : ValidationAttribute
    {

        public SoloNumeros() : base("{0} Los campos ingresados deben ser solo numeros")
        {

        }

        protected override ValidationResult IsValid(object valorPropiedad, ValidationContext ctx)
        {
            Boolean encontrado = false;

            if (valorPropiedad != null)
            {
                var cadena = valorPropiedad.ToString();
                char[] caracteres = cadena.ToCharArray();
                for (int i = 0; i < cadena.Length && !encontrado; i++)
                {
                    if (caracteres[i] == '0' || caracteres[i] == '1' || caracteres[i] == '2' || caracteres[i] == '3' || caracteres[i] == '4' || caracteres[i] == '5' ||
                        caracteres[i] == '7' || caracteres[i] == '8' || caracteres[i] == '9' || caracteres[i] == '6')
                    {

                    }
                    else
                    {
                        encontrado = true;
                    }
                }
            }
            if (encontrado)
            {
                var mensaje = FormatErrorMessage(ctx.DisplayName);
                return new ValidationResult(mensaje);
            }

            return ValidationResult.Success;

        }


    }

    }
