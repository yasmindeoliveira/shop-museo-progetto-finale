
BEGIN TRAN DATI
SET IDENTITY_INSERT [dbo].[Products] ON
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [PictureUrl], [Price]) VALUES (1, N'Mazzo di rose', N'Un semplice mazzo di rose', N'https://www.ilfioraiodistefano.com/wp-content/uploads/2020/09/AdobeStock_292837618-scaled.jpeg', 4.5)
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [PictureUrl], [Price]) VALUES (2, N'Kit di penne stilografiche', N'Un kit di sei penne stilografiche con stili diversi', N'https://penmuseum.org.uk/wp-content/uploads/2021/09/pen-museum-collection-560-scaled.jpg', 5.5)
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [PictureUrl], [Price]) VALUES (3, N'Calze di Van Gogh', N'Delle calze con l''arte di Van Gogh sopra', N'https://www.seekpng.com/png/full/231-2311510_itgirl-shop-starry-night-van-gogh-socks-aesthetic.png', 3.35)
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [PictureUrl], [Price]) VALUES (4, N'Taccuino della natura', N'Un taccuino con ornamenti a stile naturale', N'https://cdn.pixabay.com/photo/2014/08/17/16/33/notebook-420011_960_720.jpg', 4.45)
INSERT INTO [dbo].[Products] ([Id], [Name], [Description], [PictureUrl], [Price]) VALUES (5, N'La penna di Van Gogh', N'Una meravigliosa penna stilizzata con i lavori dell''artista Van Gogh', N'https://www.lastilograficamilano.it/image/cache/wp/gj/importazione/p/1340-1782-900x450.webp', 6.5)
SET IDENTITY_INSERT [dbo].[Products] OFF
COMMIT