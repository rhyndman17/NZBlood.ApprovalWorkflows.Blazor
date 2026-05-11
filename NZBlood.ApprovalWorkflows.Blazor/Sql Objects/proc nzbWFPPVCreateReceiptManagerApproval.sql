USE [NZBS]
GO

/****** Object:  StoredProcedure [dbo].[nzbWFPPVCreateReceiptManagerApproval]    Script Date: 09-May-26 11:38:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE proc [dbo].[nzbWFPPVCreateReceiptManagerApproval] @receiptNumber char(17),@managerAccountName varchar(100),@managerEmailAddress varchar(100),
				@comment varchar(max),@userId varchar(100),@isManager int,@isAdmin int
as

/* delete non-manager approval list
*/
delete nzbWFPPVApproverList where ReceiptNumber=@receiptNumber
/* add manager approval
*/
/*
insert into nzbWFPPVApproverList
select @receiptNumber,@managerAccountName,@managerEmailAddress,@managerAccountName
*/
insert into nzbWFPPVApproverList
select @receiptNumber,rtrim(ManagerAdAccount),rtrim(ManagerEmailAddress),rtrim(ManagerAdAccount )
from nzbPPVSetup
where Active='Yes' and ApprovalID='PPV1'

/* update history
*/
insert into nzbWFPPVInvoiceHistory select @receiptNumber,@userId,getdate(),2,@comment,@isManager,@isAdmin
GO


