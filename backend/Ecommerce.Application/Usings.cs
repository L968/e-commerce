global using MediatR;
global using AutoMapper;
global using FluentResults;
global using FluentValidation;
global using System.ComponentModel.DataAnnotations;

global using Ecommerce.Domain.Entities;
global using Ecommerce.Domain.Entities.CartEntities;
global using Ecommerce.Domain.Entities.OrderEntities;
global using Ecommerce.Domain.Entities.ProductEntities;

global using Ecommerce.Domain.Interfaces.CartRepositories;
global using Ecommerce.Domain.Interfaces.OrderRepositories;
global using Ecommerce.Domain.Interfaces.ProductRepositories;

global using Ecommerce.Application.Common.Interfaces;

global using Ecommerce.Application.DTO.ProductDto;
global using Ecommerce.Application.DTO.ProductInventoryDto;

global using Ecommerce.Application.Interfaces.ProductServices;