USE [NZBS]
GO

/****** Object:  StoredProcedure [dbo].[nzbWFPPVReceiptReject]    Script Date: 09-May-26 11:37:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[nzbWFPPVReceiptReject] @receiptNumber char(17),@comment varchar(max),@userId varchar(100),@isManager int,@isAdmin int 
as
declare 
	@batchId char(15) = 'REJECT',
	@chk int

/* create or update the batch
*/
set @chk=(select count(*) from SY00500 where BACHNUMB=@batchId and BCHSOURC='Rcvg Trx Ivc')
if @chk=0 begin
	execute taCreateUpdateBatchHeaderRcd
		@I_vBACHNUMB=@batchId,
		@I_vBCHCOMNT='Approval Workflow Rejects',
		@I_vSERIES=4,
		@I_vBCHSOURC='Rcvg Trx Ivc',
		@I_vDOCAMT=0,
		@I_vORIGIN=2,
		@I_vNUMOFTRX=0,
		@I_vPOSTTOGL=1,
		@I_vTRXSOURC='',		
		@O_iErrorState=0,
		@oErrString=0
end

update POP10300 set POPTYPE=2,BACHNUMB=@batchId where POPRCTNM=@receiptNumber
update SY00500 set NUMOFTRX=(select count(*) from POP10300 where BACHNUMB=@batchId) where BACHNUMB=@batchId

/* delete transactions
*/
delete nzbWFPPVInvoiceStatus	where ReceiptNumber=@receiptNumber
delete nzbWFPPVTransactionHdr	where ReceiptNumber=@receiptNumber 
delete nzbWFPPVTransactionLne	where ReceiptNumber=@receiptNumber
delete nzbWFPPVApproverList		where ReceiptNumber=@receiptNumber
/* update history
*/
insert into nzbWFPPVInvoiceHistory select @receiptNumber,@userId,getdate(),3,@comment,@isManager,@isAdmin

GO


