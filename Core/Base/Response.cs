
namespace Core.Base
{
    public record Response(
    int error,
    String message,
    object? data
);
}
