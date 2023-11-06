using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwaggerAds.Data;
using SwaggerAds.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace SwaggerAds.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("AllowAll")]

public class AdsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public AdsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    // READ ALL ///////////////////////////////////////////////////////
    /// <summary>
    /// Retrieve ALL ads from the database
    /// </summary>
    /// <returns>
    /// A full list of ALL ads
    /// </returns>
    /// <remarks>
    /// Example end point: GET /api/ads
    /// </remarks>
    /// <response code="200">
    /// Successfully returned a full list of ALL ads
    /// </response>
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<List<Ads>>> GetAll()
    {
        return Ok(await _dbContext.AdContext.ToListAsync());
    }

    // GET ONE ///////////////////////////////////////////////////////

    /// <summary>
    /// Retrieve one advert
    /// </summary>
    /// <returns>
    /// One advert
    /// </returns>
    /// <remarks>
    /// Eaxmple end point: Get /api/Ads?id=4
    /// </remarks>
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    [Route("{id}")]
    public async Task<ActionResult<Ads>> GetAd(int id)
    {
        var advert = await _dbContext.AdContext.FindAsync(id);

        if(advert == null){
            return BadRequest("Advert not found");
        }
        return Ok(advert);
    }

    // POST ONE ///////////////////////////////////////////////////////

    /// <summary>
    /// Create one advert
    /// </summary>
    /// <returns>
    /// Nada
    /// </returns>
    /// <remarks>
    /// Eaxmple end point: Post /api/Ads
    /// </remarks>
    /// 
    [HttpPost]
    [Authorize(Roles = "Admin, User")]
    public ActionResult<Ads> CreateAd(Ads newAd)
    {
        _dbContext.AdContext.Add(newAd);
        _dbContext.SaveChanges();
        return Ok(newAd);

    }

    // DELETE ONE ///////////////////////////////////////////////////////

    /// <summary>
    /// Delete one advert
    /// </summary>
    /// <returns>
    /// Nada
    /// </returns>
    /// <remarks>
    /// Eaxmple end point: Get /api/Ads?id=4
    /// </remarks>
    /// 
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<ActionResult<Ads>> DeleteAd(int id)
    {
        var adToDelete = _dbContext.AdContext.First(x => x.Id == id);
        if(adToDelete == null)
        {
            return BadRequest("Advert not found");
        }
        _dbContext.AdContext.Remove(adToDelete);
        _dbContext.SaveChanges();
        return Ok("Deleted "+adToDelete);   
    }
    // Update ONE ///////////////////////////////////////////////////////

    /// <summary>
    /// Update one entire advert
    /// </summary>
    /// <returns>
    /// Nada
    /// </returns>
    /// <remarks>
    /// Eaxmple end point: Get /api/Ads?id=4
    /// </remarks>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<IActionResult> UpateAdvert(int id, Ads advert)
    {
        var advertToUpdate = _dbContext.AdContext.First(x => x.Id == id);
        if(advertToUpdate == null)
        {
            return BadRequest("Advert not found");
        }
        advertToUpdate.Description = advert.Description;
        advertToUpdate.ImageUrl = advert.ImageUrl;
        advertToUpdate.Price = advert.Price;
        advertToUpdate.SellDAte = advert.SellDAte;
        advertToUpdate.Title = advert.Title;
        await _dbContext.SaveChangesAsync();
        return Ok(advertToUpdate);
    }
    [HttpPatch]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<ActionResult<Ads>> PatchAdd(JsonPatchDocument advert, int id)
    {
        var advertToUpdate = await _dbContext.AdContext.FindAsync(id);

        if(advertToUpdate == null)
        {
            return BadRequest("Advert not found"); 
        }

        advert.ApplyTo(advertToUpdate);
        await _dbContext.SaveChangesAsync();

        return Ok(await _dbContext.AdContext.ToListAsync());   
    }
} 