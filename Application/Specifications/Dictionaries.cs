using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class ErrorDictionaries
    {
        public Dictionary<string, string> ErrorDescriptions = new();

        public ErrorDictionaries()
        {
            // Errores de autenticación
            this.ErrorDescriptions.Add(ConstErrorCode.Auth401, "Usuario o contraseña incorrecta");
            this.ErrorDescriptions.Add(ConstErrorCode.Forbidden403, "Acceso denegado");

            // Errores de solicitud incorrecta
            this.ErrorDescriptions.Add(ConstErrorCode.BadRequest400, "Solicitud incorrecta");
            this.ErrorDescriptions.Add(ConstErrorCode.NotFound404, "Recurso no encontrado");

            // Errores de servidor interno
            this.ErrorDescriptions.Add(ConstErrorCode.ServerError500, "Error interno del servidor");

            this.ErrorDescriptions.Add(ConstErrorCode.Create400, "No se pudo crear el usuario");
            this.ErrorDescriptions.Add(ConstErrorCode.Create409, "El usuario ya existe");

            this.ErrorDescriptions.Add(ConstErrorCode.Update400, "No se pudo actualizar el registrp");
            this.ErrorDescriptions.Add(ConstErrorCode.Update404, "No se pudo encontrar el registro a actualizar");

            this.ErrorDescriptions.Add(ConstErrorCode.Delete400, "No se pudo eliminar el registro");
            this.ErrorDescriptions.Add(ConstErrorCode.Delete404, "No se pudo encontrar el registro a eliminar");
        }
    }
}
