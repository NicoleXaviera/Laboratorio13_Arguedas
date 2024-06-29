namespace Laboratorio13_Arguedas.Roles
{
    public static class ValidationHelper
    {
        public static UserValidationResult GetRole(string username, string password)
        {
            UserValidationResult validationResult = new UserValidationResult();

            // Aqu� puedes realizar la l�gica para validar el nombre de usuario y la contrase�a
            // y asignar el rol correspondiente seg�n la l�gica de tu aplicaci�n.

            // Ejemplo de validaci�n b�sica:
            if (username == "tecsup" && password == "123456")
            {
                validationResult.IsValid = true;
                validationResult.Role = "Administrator";
            }
            else if (username == "superuser" && password == "superuser123")
            {
                validationResult.IsValid = true;
                validationResult.Role = "SuperUser";
            }
            else if (username == "invited" && password == "invited123")
            {
                validationResult.IsValid = true;
                validationResult.Role = "Invited";
            }
            else
            {
                validationResult.IsValid = false;
                validationResult.Role = "User";
            }

            return validationResult;
        }
    }
}
