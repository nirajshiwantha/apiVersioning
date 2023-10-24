using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VersioningDemo.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("2.1")]
    public class BlogController : ControllerBase
    {
        private Dictionary<int, string> blogs;

        public BlogController()
        {
            blogs = new Dictionary<int, string>
            {
                { 1, "Discovering New Destinations: Travel Blog" },
                { 2, "Delicious Recipes for Food Lovers" },
                { 3, "Living a Healthy Lifestyle: Wellness Insights" },
                { 4, "Exploring the World of Literature" },
                { 5, "Fashion Trends and Style Tips" },
                { 6, "Crafting and DIY Projects for Creatives" },
                { 7, "Mindfulness and Wellness Practices" }
            };
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public IActionResult GetV1()
        {
            var apiVersion = HttpContext.GetRequestedApiVersion()!; // Using the null-forgiving operator
            var result = new
            {
                ApiVersion = apiVersion.ToString(),
                Body = blogs
            };
            return Ok(result);
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult GetV2()
        {
            var apiVersion = HttpContext.GetRequestedApiVersion()!; // Using the null-forgiving operator
            var first4Blogs = blogs.Take(4).ToDictionary(pair => pair.Key, pair => pair.Value);
            var result = new
            {
                ApiVersion = apiVersion.ToString(),
                Body = first4Blogs
            };
            return Ok(result);
        }

        [HttpGet]
        [MapToApiVersion("2.1")]
        public IActionResult GetV21()
        {
            var apiVersion = HttpContext.GetRequestedApiVersion()!; // Using the null-forgiving operator
            var first2Blogs = blogs.Take(2).ToDictionary(pair => pair.Key, pair => pair.Value);
            var result = new
            {
                ApiVersion = apiVersion.ToString(),
                Body = first2Blogs
            };
            return Ok(result);
        }
    }
}
