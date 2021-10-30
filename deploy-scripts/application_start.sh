sudo dotnet restore /var/www/DSCC_CW_8402_API/AnimationCollectionAPI/AnimationCollectionAPI.csproj
sudo dotnet publish -c release /var/www/DSCC_CW_8402_API/AnimationCollectionAPI/AnimationCollectionAPI.csproj -o /var/www/DSCC_CW_8402_API_publish/
systemctl enable website.service
systemctl start website.service