using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class QuoteController
{
    private QuoteService _quoteService;

    public QuoteController()
    {
        _quoteService = new QuoteService();
    }

    [HttpGet("GetAll")]
    public List<QuoteDto> GetQuotes()
    {
        return _quoteService.GetQuotes();
    }

    [HttpGet("GetQuotesByCategoryId")]
    public List<QuoteDto> GetQuotesByCategoryId(int id)
    {
        return _quoteService.GetQuotesByCategoryId(id);
    } 
    
    [HttpGet("GetRandomQuote")]
    public QuoteDto GetRandomQuote()
    {
        return _quoteService.GetRandomQuote();
    } 
    
    [HttpPost("AddQuote")]
    public int AddCategory(QuoteDto quoteDto)
    {
        return _quoteService.AddQuotes(quoteDto);
    }
    
    
    [HttpPut("UpdateQuote")]
    public int UpdateQuote(QuoteDto quoteDto)
    {
        return _quoteService.UpdateQuotes(quoteDto);
    }
    



}