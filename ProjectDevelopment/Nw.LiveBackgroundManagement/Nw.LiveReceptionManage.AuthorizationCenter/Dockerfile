#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
COPY . /app
EXPOSE 80
EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["Nw.LiveReceptionManage.AuthorizationCenter/Nw.LiveReceptionManage.AuthorizationCenter.csproj", "Nw.LiveReceptionManage.AuthorizationCenter/"]
#COPY ["Nw.LiveBackgroundManagement.Business.Service/Nw.LiveBackgroundManagement.Business.Service.csproj", "Nw.LiveBackgroundManagement.Business.Service/"]
#COPY ["Nw.LiveBackgroundManagement.Common/Nw.LiveBackgroundManagement.Common.csproj", "Nw.LiveBackgroundManagement.Common/"]
#COPY ["RedisHelper/RedisHelper.csproj", "RedisHelper/"]
#COPY ["Nw.LiveBackgroundManagement.DataAccessEFCore/Nw.LiveBackgroundManagement.DataAccessEFCore.csproj", "Nw.LiveBackgroundManagement.DataAccessEFCore/"]
#COPY ["Nw.LiveBackgroundManagement.Models/Nw.LiveBackgroundManagement.Models.csproj", "Nw.LiveBackgroundManagement.Models/"]
#COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
#COPY ["EventBus/EventBus.csproj", "EventBus/"]
#COPY ["Nw.LiveBackgroundManagement.Business.Interface/Nw.LiveBackgroundManagement.Business.Interface.csproj", "Nw.LiveBackgroundManagement.Business.Interface/"]
#RUN dotnet restore "Nw.LiveReceptionManage.AuthorizationCenter/Nw.LiveReceptionManage.AuthorizationCenter.csproj"
#COPY . .
#WORKDIR "/src/Nw.LiveReceptionManage.AuthorizationCenter"
#RUN dotnet build "Nw.LiveReceptionManage.AuthorizationCenter.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Nw.LiveReceptionManage.AuthorizationCenter.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Nw.LiveReceptionManage.AuthorizationCenter.dll"]