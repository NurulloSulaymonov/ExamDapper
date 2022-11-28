using Dapper;
using Domain.Dtos;
using Npgsql;

namespace Infrastructure.Services;

public class QuoteService
{
    private string _connectionString = "Server=127.0.0.1;Port=5432;Database=dapper_demo;User Id=postgres;Password=12345;";

    public List<QuoteDto> GetQuotes()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            
            var quotes = connection.Query<QuoteDto>("SELECT *  FROM quotes").ToList();
            return quotes;
        }
    }
    
    public List<QuoteDto> GetQuotesByCategoryId(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            
            var quotes = connection.Query<QuoteDto>($"SELECT *  FROM quotes where categoryId = {id}").ToList();
            return quotes;
        }
    }
    
    public QuoteDto GetRandomQuote()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var quotes = connection.QueryFirstOrDefault<QuoteDto>("select * from quotes order by random() limit 1;");
            return quotes;
        }
    }
    
    public int AddQuotes(QuoteDto quote)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {

            var quotes =
                connection.Execute(
                    $"Insert into quotes (Author,QuoteText,CategoryId) values ('{quote.Author}','{quote.QuoteText}',{quote.CategoryId})");
            
            return quotes;
        }
    }
    
    public int UpdateQuotes(QuoteDto quote)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {

            var quotes =
                connection.Execute(
                    $"Update quotes set Author = '{quote.Author}', QuoteText = '{quote.QuoteText}', CategoryId = {quote.CategoryId} where id = {quote.Id}");
            
            return quotes;
        }
    }
    
    public int DeleteQuotes(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {

            var quotes =
                connection.Execute(
                    $"delete from quotes where id = {id}");
            
            return quotes;
        }
    }
}