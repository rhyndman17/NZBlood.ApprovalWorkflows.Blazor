USE [NZBS]
GO

/****** Object:  StoredProcedure [dbo].[nzbWFPPVReceiptApproval]    Script Date: 09-May-26 11:37:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE proc [dbo].[nzbWFPPVReceiptApproval] @receiptNumber char(17),@comment varchar(max),@userId varchar(100),@isManager int,@isAdmin int 
as
declare 
	@batchId char(15) = 'APPR-' + rtrim(@receiptNumber),
	@chk int,
	@batchComment varchar(60)='Approval Workflow Approved - ' + rtrim(@receiptNumber),
	@glPostDate date = convert(date,getdate())

/* create or update the batch
*/
set @chk=(select count(*) from SY00500 where BACHNUMB=@batchId and BCHSOURC='Rcvg Trx Ivc')
if @chk=0 begin
	execute taCreateUpdateBatchHeaderRcd
		@I_vBACHNUMB=@batchId,
		@I_vBCHCOMNT=@batchComment,
		@I_vSERIES=4,
		@I_vBCHSOURC='Rcvg Trx Ivc',
		@I_vDOCAMT=0,
		@I_vORIGIN=2,
		@I_vNUMOFTRX=1,
		@I_vPOSTTOGL=1,
		@I_vTRXSOURC='',
		@I_vGLPOSTDT=@glPostDate,
		@O_iErrorState=0,
		@oErrString=0
end

update POP10300 set POPTYPE=2,BACHNUMB=@batchId where POPRCTNM=@receiptNumber
--update SY00500 set NUMOFTRX=(select count(*) from POP10300 where BACHNUMB=@batchId) where BACHNUMB=@batchId

/* delete transactions
*/
delete nzbWFPPVInvoiceStatus	where ReceiptNumber=@receiptNumber
delete nzbWFPPVTransactionHdr	where ReceiptNumber=@receiptNumber 
delete nzbWFPPVTransactionLne	where ReceiptNumber=@receiptNumber
delete nzbWFPPVApproverList		where ReceiptNumber=@receiptNumber

/* update history
*/
insert into nzbWFPPVInvoiceHistory select @receiptNumber,@userId,getdate(),2,@comment,@isManager,@isAdmin
/* post batch
*/
delete hsPostingPreventions where HS_Invoice_Number=@receiptNumber and HS_Posting_Prev_Type=2
insert into DYNAMICS..ESS80000 select DB_NAME(),@batchId,'Rcvg Trx Ivc',4,0,getdate(),getdate(),0,'',0,@@SERVERNAME,0
GO


