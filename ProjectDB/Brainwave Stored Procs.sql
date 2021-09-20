
IF EXISTS(SELECT 1 FROM sys.procedures 
          WHERE Name = 'GetCompanyProductsByCompanyID')
BEGIN
    DROP PROCEDURE dbo.[GetCompanyProductsByCompanyID]
END
go
create PROCEDURE [dbo].[GetCompanyProductsByCompanyID]
@CompanyID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select op.price, op.order_id, op.product_id, op.quantity, p.name, p.price as productPrice 
	from [BrainWAre].[dbo].[Order] as o 
	inner join [BrainWAre].[dbo].orderproduct as op on o.order_id = op.order_id 
	inner join [BrainWAre].[dbo].Product as p on p.product_id = op.product_id where o.company_id = @CompanyID
END
GO



IF EXISTS(SELECT 1 FROM sys.procedures 
          WHERE Name = 'GetCompanyOrdersByID')
BEGIN
    DROP PROCEDURE dbo.[GetCompanyOrdersByID]
END
go
create PROCEDURE [dbo].[GetCompanyOrdersByID]
@CompanyID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT c.name, o.description, o.order_id FROM company c INNER JOIN [order] o on c.company_id=o.company_id where c.company_id = @CompanyID
END

GO
