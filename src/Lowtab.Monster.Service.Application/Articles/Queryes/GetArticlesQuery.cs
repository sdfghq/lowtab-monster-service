using Mediator;
using Lowtab.Monster.Service.Contracts.Articles.Common;
using Lowtab.Monster.Service.Contracts.Articles.GetArticles;

namespace Lowtab.Monster.Service.Application.Articles.Queryes;

/// <summary>
///     Запрос для получения объекта <see cref="ArticleBase"/>
/// </summary>
public record GetArticlesQuery : GetArticlesRequest, IQuery<GetArticlesResponse>;
