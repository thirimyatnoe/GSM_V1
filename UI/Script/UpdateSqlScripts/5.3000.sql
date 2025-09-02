Update tbl_Version Set VersionNo='5.3000',VersionDate=GETDATE();
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_SalesVolumeDetail' and column_name = 'DesignCharges')
alter table tbl_SalesVolumeDetail add  DesignCharges Integer NUll 
GO
Update tbl_SalesVolumeDetail Set DesignCharges=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_wholesaleinvoiceItem' and column_name = 'DesignCharges')
alter table tbl_wholesaleinvoiceItem add  DesignCharges Integer NUll 
GO
Update tbl_wholesaleinvoiceItem Set DesignCharges=0;
GO
if not exists (select column_name from INFORMATION_SCHEMA.columns where table_name = 'tbl_wholesaleinvoice' and column_name = 'TotalDesignCharges')
alter table tbl_wholesaleinvoice add  TotalDesignCharges Integer NUll 
GO
Update tbl_wholesaleinvoice Set TotalDesignCharges=0;
GO
