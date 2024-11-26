namespace WebApp_SistemaBiblioteca.Helpers
{
    public static class CantidadMaximaPrestamo
    {
        public static DateTime FechaMaximaPrestamo(int idTipoUsuario)
        {
            DateTime fechaMaxima = DateTime.Now;

            switch (idTipoUsuario)
            {
                case 1://Afiliado
                    fechaMaxima = fechaMaxima.AddDays(10);
                    break;
                case 2://Empleado o Estudiante
                    fechaMaxima = fechaMaxima.AddDays(8);
                    break;
                case 3://Inivtado
                    fechaMaxima = fechaMaxima.AddDays(7);
                    break;
            }

            if (fechaMaxima.DayOfWeek == DayOfWeek.Saturday)
            {
                fechaMaxima = fechaMaxima.AddDays(2);//Lunes
            }
            else if (fechaMaxima.DayOfWeek == DayOfWeek.Sunday)
            {
                fechaMaxima = fechaMaxima.AddDays(1);//Lunes
            }
            return fechaMaxima;
        }
    }
}
