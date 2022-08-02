global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Cors;
global using Microsoft.AspNetCore.Cors.Infrastructure;

global using Microsoft.IdentityModel.Tokens;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;

global using System.Text;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Diagnostics;
global using System.Security.Claims;
global using System.IdentityModel.Tokens.Jwt;

global using WebShop.Data;
global using WebShop.Models;
global using WebShop.Models.Base;
global using WebShop.Models.Binding;
global using WebShop.Models.Dbo;
global using WebShop.Models.Dbo.Base;
global using WebShop.Models.Dto;
global using WebShop.Models.ViewModel;
global using CorsPolicy = WebShop.Models.CorsPolicy;

global using WebShop.Services.Interface;
global using WebShop.Services.Implementation;

global using AutoMapper;