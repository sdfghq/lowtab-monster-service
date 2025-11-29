using System.Diagnostics.CodeAnalysis;

namespace Lowtab.Monster.Service.Application.Common.Exceptions;

/// <summary>
///     Исключение, которое выбрасывается, если объект не найден
/// </summary>
/// <param name="message"></param>
[SuppressMessage("Design", "CA1032:Реализуйте стандартные конструкторы исключения")]
[SuppressMessage("Roslynator", "RCS1194:Implement exception constructors")]
public class NotFoundException(string message) : Exception(message);
