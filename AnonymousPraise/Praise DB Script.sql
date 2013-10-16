Use Praise
GO

--Praise
CREATE TABLE [dbo].[Praise] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Comment]     VARCHAR (140) NOT NULL,
    [CreatedDate] DATE          NOT NULL,
    [Moderated]   BIT           NOT NULL,
    [PersonId]    INT           NULL,
    CONSTRAINT [PK_Praise] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

--People
CREATE TABLE [dbo].[People] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (80) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE NONCLUSTERED INDEX [ix_people_Name]
    ON [dbo].[People]([Name] ASC);

GO

--Likes
CREATE TABLE [dbo].[Likes] (
    [PraiseId] INT  NOT NULL,
    [Date]     DATE NOT NULL,
    CONSTRAINT [FK_Likes_Praise] FOREIGN KEY ([PraiseId]) REFERENCES [dbo].[Praise] ([Id])
);

GO
CREATE CLUSTERED INDEX [IX_Likes_PraiseId]
    ON [dbo].[Likes]([PraiseId] ASC);
GO

CREATE PROCEDURE dbo.spAddPraise 
(
	@Person varchar(80),
	@Comment varchar(140)
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @id int
	
	select @id = [people].id 
			from [dbo].[People] with (nolock)
			Where [people].Name = @Person
	
	Insert Into Praise(Comment, CreatedDate, Moderated, PersonId) 
		Values(@Comment, GetDate(), 0, @id) 
	select @@IDENTITY

END
GO

CREATE  PROCEDURE dbo.spGetPraise
(
	@Person varchar(80) = null,
	@Moderated bit = 1,
    @PageNumber int = 1,
    @PageSize int = 20
)
AS

declare @lower int =  (@PageNumber * @PageSize) - (@PageSize - 1)
declare @upper int =  (@PageNumber    ) * @PageSize

select * from (
	select 
		ROW_NUMBER() over (order by [praise].Id desc) lfd
		,[praise].Id
		,min([people].Name) as Person
		,min(Comment) as Comment
		,min(CreatedDate) as CreatedDate
		,Moderated
		,count(praiseId) as Likes
	FROM [Praise].[dbo].[Praise] with (nolock)
	join [Praise].[dbo].[people] with (nolock) on [people].Id = [praise].PersonId
	left join praise.dbo.likes with (nolock) on [praise].id = praiseid
	where Moderated = @Moderated
	  and [people].Name = isnull(@Person, [people].Name)
	group by [praise].Id, moderated
) as t
 where lfd between @lower and @upper
 order by t.Id desc
 GO