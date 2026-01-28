using AutoMapper;
using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CatalogService> _logger;
        private readonly IAnimeService _animeService;

        public CatalogService(
            ICatalogRepository catalogRepository, 
            IMapper mapper,
            ILogger<CatalogService> logger,
            IAnimeService animeService)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
            _logger = logger;
            _animeService = animeService;
        }

        public async Task<IEnumerable<CatalogResponse>> GetCatalogsAsync(string? query = null)
        {
            _logger.LogInformation("Fetching catalogs with query: {Query}", query);
            try
            {
                var catalogs = await _catalogRepository.GetAllAsync();

                if (!string.IsNullOrWhiteSpace(query))
                {
                    catalogs = catalogs.Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
                }
                
                var responseList = new List<CatalogResponse>();

                foreach (var catalog in catalogs)
                {
                    string imageUrl = string.Empty;
                    int animeId = 0;
                    try 
                    {
                         var searchResult = await _animeService.SearchAnimeAsync(catalog.Name);
                         var firstMatch = searchResult.FirstOrDefault();
                         if (firstMatch != null)
                         {
                             imageUrl = firstMatch.ImageUrl ?? string.Empty;
                             animeId = firstMatch.Id;
                         }
                    }
                    catch(Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to fetch image for catalog {CatalogName}", catalog.Name);
                    }

                    responseList.Add(new CatalogResponse
                    {
                        OperationId = catalog.OperationId,
                        Name = catalog.Name,
                        Description = catalog.Description,
                        ImageUrl = imageUrl,
                        AnimeId = animeId
                    });
                }

                _logger.LogInformation("Successfully fetched and enriched {Count} catalogs", responseList.Count);
                return responseList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all catalogs");
                throw;
            }
        }

        public async Task<Catalog?> GetCatalogAsync(string id)
        {
            _logger.LogInformation("Fetching catalog with ID: {CatalogId}", id);
            try
            {
                var catalog = await _catalogRepository.GetByIdAsync(id);
                if (catalog == null)
                {
                    _logger.LogWarning("Catalog not found with ID: {CatalogId}", id);
                }
                else
                {
                    _logger.LogInformation("Successfully fetched catalog with ID: {CatalogId}", id);
                }
                return catalog;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching catalog with ID: {CatalogId}", id);
                throw;
            }
        }

        public async Task CreateCatalogAsync(CatalogRequest catalogRequest)
        {
            _logger.LogInformation("Creating new catalog with name: {CatalogName}", catalogRequest.Name);
            try
            {
                var catalog = _mapper.Map<Catalog>(catalogRequest);
                
                // Ensure Id is null to let MongoDB generate a valid ObjectId
                catalog.Id = null;
                
                // Generate OperationId if not provided
                if (catalog.OperationId == Guid.Empty)
                {
                    catalog.OperationId = Guid.NewGuid();
                    _logger.LogInformation("Generated new OperationId: {OperationId} for catalog: {CatalogName}", 
                        catalog.OperationId, catalogRequest.Name);
                }

                await _catalogRepository.CreateAsync(catalog);
                _logger.LogInformation("Successfully created catalog with name: {CatalogName}, OperationId: {OperationId}", 
                    catalogRequest.Name, catalog.OperationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating catalog with name: {CatalogName}", catalogRequest.Name);
                throw;
            }
        }

        public async Task UpdateCatalogAsync(string id, CatalogRequest catalogRequest)
        {
            _logger.LogInformation("Updating catalog with ID: {CatalogId}", id);
            try
            {
                var existing = await _catalogRepository.GetByIdAsync(id);
                if (existing != null)
                {
                    _mapper.Map(catalogRequest, existing);
                    existing.Id = id;
                    
                    await _catalogRepository.UpdateAsync(id, existing);
                    _logger.LogInformation("Successfully updated catalog with ID: {CatalogId}, Name: {CatalogName}", 
                        id, catalogRequest.Name);
                }
                else
                {
                    _logger.LogWarning("Attempted to update non-existent catalog with ID: {CatalogId}", id);
                    throw new InvalidOperationException($"Catalog with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating catalog with ID: {CatalogId}", id);
                throw;
            }
        }

        public async Task DeleteCatalogAsync(string id)
        {
            _logger.LogInformation("Deleting catalog with ID: {CatalogId}", id);
            try
            {
                await _catalogRepository.DeleteAsync(id);
                _logger.LogInformation("Successfully deleted catalog with ID: {CatalogId}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting catalog with ID: {CatalogId}", id);
                throw;
            }
        }
    }
}
