﻿using AutoMapper;
using Inventory.Domain.Repository.Abstract;
using Inventory.Models;
using Microsoft.AspNetCore.Components;

namespace Inventory.Pages
{
    public partial class Suppliers
    {
        [Inject] private ISupplierRepository SupplierRepository { get; set; }
        [Inject] private ILogger<Login> Logger { get; set; }
        [Inject] private IMapper Mapper { get; set; }

        private List<SupplierModel> suppliers;
        private List<SupplierModel> suppliersAfterSearch;
        private bool isSortAscending = false;

        protected async override Task OnInitializedAsync()
        {
            suppliers = new();
            suppliersAfterSearch = new();
            try
            {
                var list = await SupplierRepository.GetAll();
                if (list.Count != 0)
                {
                    suppliers = list.Select(c => Mapper.Map<SupplierModel>(c)).ToList();
                    suppliersAfterSearch = suppliers;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Suppliers page error" + ex.Message);
            }
        }
        public void SearchItem(ChangeEventArgs e)
        {
            var search = e.Value.ToString().ToLower();
            suppliersAfterSearch = suppliers.Where(n => n.SupplierId.ToLower().Contains(search) || n.Name.ToLower().Contains(search)).ToList();
        }

        public void SortItem(string column)
        {
            if (suppliersAfterSearch.Count != 0)
            {
                if (column == "SuptomerId")
                {
                    if (isSortAscending)
                    {
                        suppliersAfterSearch = suppliersAfterSearch.OrderBy(c => c.SupplierId).ToList();
                        isSortAscending = false;
                    }
                    else
                    {
                        suppliersAfterSearch = suppliersAfterSearch.OrderByDescending(c => c.SupplierId).ToList();
                        isSortAscending = true;
                    }
                }
                else if (column == "Name")
                {
                    if (isSortAscending)
                    {
                        suppliersAfterSearch = suppliersAfterSearch.OrderBy(c => c.Name).ToList();
                        isSortAscending = false;
                    }
                    else
                    {
                        suppliersAfterSearch = suppliersAfterSearch.OrderByDescending(c => c.Name).ToList();
                        isSortAscending = true;
                    }
                }
                else if (column == "Mobile")
                {
                    if (isSortAscending)
                    {
                        suppliersAfterSearch = suppliersAfterSearch.OrderBy(c => c.Name).ToList();
                        isSortAscending = false;
                    }
                    else
                    {
                        suppliersAfterSearch = suppliersAfterSearch.OrderByDescending(c => c.Name).ToList();
                        isSortAscending = true;
                    }
                }
                else if (column == "Area")
                {
                    if (isSortAscending)
                    {
                        suppliersAfterSearch = suppliersAfterSearch.OrderBy(c => c.Name).ToList();
                        isSortAscending = false;
                    }
                    else
                    {
                        suppliersAfterSearch = suppliersAfterSearch.OrderByDescending(c => c.Name).ToList();
                        isSortAscending = true;
                    }
                }
                else if (column == "Amount")
                {
                    if (isSortAscending)
                    {
                        suppliersAfterSearch = suppliersAfterSearch.OrderBy(c => c.Name).ToList();
                        isSortAscending = false;
                    }
                    else
                    {
                        suppliersAfterSearch = suppliersAfterSearch.OrderByDescending(c => c.Name).ToList();
                        isSortAscending = true;
                    }
                }
            }
        }
    }
}

