        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            base.RegisterBuilder(builder);
            builder.RegisterType<BuildUnitService>().As<IBuildUnitService>();
        }
        public override void Initialize()
        {
            base.Initialize();
            AutoMapper.Mapper.CreateMap<BuildUnitModel, BuildUnit>();
        }
 [RoleName( "台帐：管理","台帐：添加", "台帐：编辑","台帐：查询", "台帐：删除")]