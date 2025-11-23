# 1️⃣ 构建 React 前端
FROM node:20 AS frontend
WORKDIR /app/frontend
COPY frontend/package*.json ./
RUN npm install
COPY frontend/ .
RUN npm run build

# 2️⃣ 构建 .NET 后端
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish Bella_Tian__Qiu_FullStackDeveloperWork/Bella_Tian__Qiu_FullStackDeveloperWork.csproj -c Release -o /app/publish

# 3️⃣ 运行阶段
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=frontend /app/frontend/dist ./wwwroot
EXPOSE 5000
ENTRYPOINT ["dotnet", "Bella_Tian__Qiu_FullStackDeveloperWork.dll"]
