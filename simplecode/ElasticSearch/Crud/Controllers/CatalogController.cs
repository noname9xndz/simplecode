using Elasticsearch.Net;
using ElasticSearchCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearchCrud.ES;

namespace ElasticSearchCrud.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IElasticSearchFactory _esFactory;

        public CatalogController(IElasticSearchFactory esFactory)
        {
            _esFactory = esFactory;
        }

        public async Task<ActionResult> Index()
        {
            return View(await DoSearchAsync());
        }

        private async Task<List<Catalog>> DoSearchAsync(string name = "")
        {
            int page = 0;
            int pageSize = 50;
            var query = await _esFactory.ESFactory().SearchAsync<Catalog>(s => s
                .Index("catalogs")
                .From((page - 1) * pageSize)
                .Size(pageSize)
                .Query(q => q.Match(m => m.Field(f => f.Name).Query(name))));
            var totalRecord = query.HitsMetadata.Total.Value;
            var response = query.Hits.Select(s => s.Source).ToList();
            return response;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    catalog.Id = Guid.NewGuid().ToString();

                    var response = _esFactory.ESFactory().Index<Catalog>(catalog, i => i
                                           .Index("catalogs")
                                           .Id(catalog.Id)
                                           .Refresh(Elasticsearch.Net.Refresh.True));

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(catalog);
                }
            }

            return View(catalog);
        }

        // GET: Catalog/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var catalog = await GetByIdAsync(id);
            return View(catalog);
        }

        private async Task<Catalog> GetByIdAsync(string id)
        {
            try
            {
                var query = await _esFactory.ESFactory().GetAsync<Catalog>(id, i => i.Index("catalogs"));
                var result = query.Source;
                return result;
            }
            catch (ElasticsearchClientException ex)
            {
                var test = ex.Message;
            }
            return new Catalog();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _esFactory.ESFactory().UpdateAsync<Catalog>(catalog, i => i
                                           .Index("catalogs")
                                           .Refresh(Refresh.True));

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(catalog);
                }
            }

            return View(catalog);
        }

        // GET: Catalog/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var response = await _esFactory.ESFactory().DeleteAsync<Catalog>(id, i => i
                    .Index("catalogs")
                    .Refresh(Refresh.True));

                return RedirectToAction(nameof(Index));
            }
            catch (ElasticsearchClientException ex)
            {
                var test = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

/*
https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/elasticsearch-net-getting-started.html
Get :  http://127.0.0.1:9200/catalogs/_search?pretty
Post : localhost:9200/catalogs/_doc/1?pretty
raw
{
 "id": "1",
 "name":"Car",
 "description": "Car description"
}
localhost:9200/catalogs/_doc/1?pretty
GET /bookdb_index/book/_search?q=guide
GET /bookdb_index/book/_search
GET /bookdb_index/book/_search?q=title:in action
POST /bookdb_index/book/_search
https://www.elastic.co/guide/en/elasticsearch/client/net-api/2.x/filtered-query-usage.html
https://viblo.asia/p/elasticsearch-example-queries-part-01-gAm5yqVA5db
*/
