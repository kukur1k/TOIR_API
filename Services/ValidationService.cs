namespace TOIR_API.Services
{
    public static class ValidationService
    {
        public static List<String> Validate(RegisterRequest req)
        {
            var errors = new List<String>();

            if (String.IsNullOrWhiteSpace(req.Email)) errors.Add("Email обязателен");
            if (!req.Email.Contains("@")) errors.Add("Неверный формат Email");
            if (String.IsNullOrWhiteSpace(req.Password)) errors.Add("Пароль обязателен");
            if (req.Password.Length < 6) errors.Add("Пароль минимум 6 символов");
            if (String.IsNullOrWhiteSpace(req.LastName)) errors.Add("Фамилия обязательна");
            if (String.IsNullOrWhiteSpace(req.FirstName)) errors.Add("Имя обязательно");

            return errors;
        }

        public static List<String> Validate(LoginRequest req)
        {
            var errors = new List<String>();

            if (String.IsNullOrWhiteSpace(req.Email)) errors.Add("Email обязателен");
            if (String.IsNullOrWhiteSpace(req.Password)) errors.Add("Пароль обязателен");

            return errors;
        }

        public static IResult? Check(List<String> errors)
        {
            if (errors.Count > 0) return Api.BadRequest(String.Join(";", errors));
            else
            {
                return null;
            }
        }
    }
}