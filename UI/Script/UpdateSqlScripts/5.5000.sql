Update tbl_Version Set VersionNo='5.5000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_saleinvoicedetail' and column_name = 'DesignChargesRate')
alter table tbl_saleinvoicedetail add  DesignChargesRate Integer NUll 
GO
Update tbl_saleinvoicedetail Set DesignChargesRate=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_salesvolumedetail' and column_name = 'DesignChargesRate')
alter table tbl_salesvolumedetail add  DesignChargesRate Integer NUll 
GO
Update tbl_salesvolumedetail Set DesignChargesRate=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_WholesaleInvoiceitem' and column_name = 'DesignChargesRate')
alter table tbl_WholesaleInvoiceitem add  DesignChargesRate Integer NUll 
GO
Update tbl_WholesaleInvoiceitem Set DesignChargesRate=0;
GO
