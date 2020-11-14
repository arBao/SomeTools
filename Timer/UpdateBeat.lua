---用来作为UpdateBeat实例便于封装
local UpdateBeatInstance = {
	name = 'UpdateBeat',
	gameObject = nil,
}

function UpdateBeatInstance:GetInstance()
    if self.instance == nil then
        self.instance = CS.UpdateBeat.Add(UpdateBeatInstance)
    end
    return self.instance
end

function UpdateBeatInstance:New()
    local o = {}
	setmetatable(o,self)
	self.__index = self
	return o
end

function UpdateBeatInstance:Awake()
    self.id = 0
    self.listWaitToDelete = {}
    self.dicUpdateFuncs = {}
    -- logError("function UpdateBeat:Awake()")
end

function UpdateBeatInstance:Start()
    -- logError("function UpdateBeat:Start()")
end

function UpdateBeatInstance:OnEnable()
    -- logError("function UpdateBeat:OnEnable()")
end

function UpdateBeatInstance:OnDisable()
    -- logError("function UpdateBeat:OnDisable()")
end

-- function UpdateBeat:FixedUpdate(fixedDeltaTime)
--     logError("function UpdateBeat:FixedUpdate()")
    -- Time.SetFixedDelta(fixedDeltaTime)
-- end

-- function UpdateBeat:LateUpdate()
--     logError("function UpdateBeat:LateUpdate()")
-- end

function UpdateBeatInstance:OnDestroy()
    -- logError("function UpdateBeat:OnDestroy()")
end

local function DeleteUpdateFuncs(self)
    if #self.listWaitToDelete == 0 then
        return
    end

    for i=1,#self.listWaitToDelete,1 do
        self.dicUpdateFuncs[self.listWaitToDelete[i]] = nil
    end
    self.listWaitToDelete = {}
end

--func(deltaTime)
function UpdateBeatInstance:Add(func)
    if func == nil then
        return
    end
    self.id = self.id + 1
    self.dicUpdateFuncs[self.id] = func
    return self.id
end

function UpdateBeatInstance:Remove(id)
    if id == nil then
        return
    end

    if self.dicUpdateFuncs[id] ~= nil then
        table.insert(self.listWaitToDelete,id)
    end
end

function UpdateBeatInstance:Update(deltaTime,unscaledDeltaTime)
    ---在这里初始化 Time控件
    Time.SetDeltaTime(deltaTime,unscaledDeltaTime)
    Time.SetFrameCount()

    DeleteUpdateFuncs(self)

    for id,func in pairs(self.dicUpdateFuncs) do
        func(deltaTime)
        -- logError("k  " .. k)
    end

    DeleteUpdateFuncs(self)
    -- logError("function UpdateBeat:Update() " .. Time.GetTime())
end

UpdateBeat = {}
function UpdateBeat.Add(func)
    return UpdateBeatInstance:GetInstance():Add(func)
end

function UpdateBeat.Remove(id)
    UpdateBeatInstance:GetInstance():Remove(id)
end
