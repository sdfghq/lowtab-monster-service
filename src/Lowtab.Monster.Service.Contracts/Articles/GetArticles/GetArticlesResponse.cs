using Lowtab.Monster.Service.Contracts.Common;
using Lowtab.Monster.Service.Contracts.Articles.Common;
using Lowtab.Monster.Service.Contracts.Articles.GetArticle;

namespace Lowtab.Monster.Service.Contracts.Articles.GetArticles;

/// <summary>
///     Ответ на запрос получения списка объектов <see cref="Article"/>
/// </summary>
public record GetArticlesResponse : PaginatedResponse<GetArticleResponse>;
