USE [NZBS]
GO

/****** Object:  Table [dbo].[nzbWFPPVTransactionLne]    Script Date: 09-May-26 11:34:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[nzbWFPPVTransactionLne](
	[ReceiptNumber] [char](17) NOT NULL,
	[ReceiptLineNumber] [int] NOT NULL,
	[ItemNumber] [char](31) NULL,
	[ItemDescription] [char](101) NULL,
	[VendorItemNumber] [char](31) NULL,
	[Variance] [numeric](18, 2) NULL,
	[POCost] [numeric](18, 2) NULL,
	[InvoiceCost] [numeric](18, 2) NULL,
	[PONumber] [varchar](20) NULL,
	[ShipmentNumber] [char](17) NULL,
	[POBinary] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


