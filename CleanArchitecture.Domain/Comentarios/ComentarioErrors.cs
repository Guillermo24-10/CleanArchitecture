using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Comentarios
{
    public static class ComentarioErrors
    {
        public static readonly Error NotEligible = new(
                "Review.NotEligible",
                "Este review y calificacion no es elegible por que aun no se completa"
            );
    }
}
