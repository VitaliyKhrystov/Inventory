﻿using AutoMapper;
using Inventory.Domain.Entities;
using Inventory.Domain.Repository.Abstract;
using Inventory.Models;
using Microsoft.AspNetCore.Components;
using System;

namespace Inventory.Pages
{
    public partial class Purchase
    {
        [Parameter] public string PurchaseId { get; set; }
        [Inject] private IPurchaseRepository PurchaseRepository { get; set; }
        [Inject] private ISupplierRepository SuplierRepository { get; set; }
        [Inject] private ILogger<Purchase> Logger { get; set; }
        [Inject] private NavigationManager navManager { get; set; }

        private bool isVisibleSupplierPopup = false;
        private SupplierEntity supplier = new();
        private PurchaseModel purchaseModel = new();
        private PurchaseEntity purchaseEntity = new();
        private ComponentTotalData PurchaseTotalData = new();
        private bool IsDisabled { get; set; }

        protected async override Task OnInitializedAsync()
        {
            if (PurchaseId != null)
            {
                purchaseEntity = await PurchaseRepository.GetById(Guid.Parse(PurchaseId));
                if (purchaseEntity != null)
                {
                    purchaseModel.SupplierId = purchaseEntity.Supplier.SupplierId;
                    purchaseModel.SupplierName = purchaseEntity.Supplier.Name;
                    purchaseModel.Date = purchaseEntity.Date;
                    purchaseModel.Remarks = purchaseEntity.Remarks;
                    purchaseModel.TotalAmountProduct = purchaseEntity.TotalAmountProduct;
                    purchaseModel.VoucherId = purchaseEntity.VoucherId;
                    supplier = purchaseEntity.Supplier;
                    PurchaseTotalData.Discount = purchaseEntity.Discount.GetValueOrDefault(0);
                    IsDisabled = false;
                    GetTotalAmount();
                } 
            }
            else
                IsDisabled = true;
        }
        public async Task AddPurchase()
        {
            if (purchaseModel != null)
            {
                try
                {
                    var voucherIdDb = await PurchaseRepository.GetLastVoucherIdByDate(purchaseModel.Date);
                    if (voucherIdDb == 0)
                        purchaseModel.VoucherId = 1;
                    else
                        purchaseModel.VoucherId = voucherIdDb + 1;

                    purchaseEntity = new()
                    {
                        Date = purchaseModel.Date,
                        Remarks = purchaseModel.Remarks,
                        Supplier = supplier,
                        VoucherId = purchaseModel.VoucherId
                    };
                    var id = await PurchaseRepository.Create(purchaseEntity);
                    PurchaseId = id.ToString();
                    purchaseModel.Id = id;
                    purchaseEntity = await PurchaseRepository.GetById(id);
                    IsDisabled = false;
                }
                catch (Exception ex)
                {
                    Logger.LogError("Add new purchase error: " + ex.Message);
                }
            }
        }
        public async Task EditPurchase()
        {
            if (purchaseModel != null)
            {
                try
                {
                    var isVoucherExist = false;
                    if (purchaseEntity.VoucherId != purchaseModel.VoucherId)
                        isVoucherExist = await PurchaseRepository.IsVoucherExistByDate(purchaseModel.VoucherId, purchaseModel.Date);

                    if (!isVoucherExist)
                        purchaseEntity.VoucherId = purchaseModel.VoucherId;
                    else
                        purchaseModel.VoucherId = purchaseEntity.VoucherId;

                    if (purchaseEntity.Supplier.Id != supplier.Id)
                        purchaseEntity.Supplier = supplier;

                    purchaseEntity.Remarks = purchaseModel.Remarks;
                    purchaseEntity.Date = purchaseModel.Date;

                    await PurchaseRepository.Update(purchaseEntity);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Update purchase: " + ex.Message);
                }
            }
        }

        public async Task DeletePurchase()
        {
            if (purchaseEntity != null)
            {
                try
                {
                    await PurchaseRepository.Delete(purchaseEntity);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Delete purchase: " + ex.Message);
                }
                navManager.NavigateTo("/purchases");
            }
        }

        public async Task AddTotalAmount()
        {
            try
            {
                purchaseEntity.TotalAmountProduct = PurchaseTotalData.TotalAmount;
                purchaseEntity.Discount = PurchaseTotalData.Discount;
                await PurchaseRepository.Update(purchaseEntity);
                navManager.NavigateTo("/purchases");
            }
            catch (Exception ex)
            {
                Logger.LogError("Add total amount: " + ex.Message);
            }
        }

        public void OpenSupplierPopup()
        {
            isVisibleSupplierPopup = true;
        }
        public void CloseSupplierPopup(bool state)
        {
            isVisibleSupplierPopup = state;
        }
        public async void GetSupplierFromPopup(SupplierModel supplierFromPopup)
        {
            if (supplierFromPopup != null)
            {
                purchaseModel.SupplierId = supplierFromPopup.SupplierId;
                purchaseModel.SupplierName = supplierFromPopup.Name;
                supplier = await SuplierRepository.GetById(supplierFromPopup.Id);
            }
        }

        private void DiscountChanged(decimal value)
        {
            PurchaseTotalData.Discount = value;
            GetTotalAmount();
        }

        public void GetTotalAmount()
        {
            PurchaseTotalData.TotalQuantity = purchaseEntity.PurchaseVariants.Select(x => x.Quantity).Sum().Value;
            var amount = purchaseEntity.PurchaseVariants.Select(v => v.AmountAfterDiscount).Sum().Value;
            PurchaseTotalData.TotalAmount = amount - PurchaseTotalData.Discount;
        }

        public void ChangeStatePurchaseVariant(bool change)
        {
            if (change)
                GetTotalAmount();
        }
    }
}
