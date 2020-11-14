---@class XTweenObserver
XTweenObserver = {
    name = 'XTweenObserver',
    gameObject = nil,
}

function XTweenObserver:New()
    local o = {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---@return XTweener[]
function XTweenObserver:GetListTweeners()
    self.listTweeners = self.listTweeners or {}
    return self.listTweeners
end

function XTweenObserver:Awake()

end

---@param xTweener XTweener
function XTweenObserver:AddXTweener(xTweener)
    --local replace = false
    local list = self:GetListTweeners()
    for i = 1, #list do
        local tmpXTweener = list[i]
        if tmpXTweener:GetTypeMark() == xTweener:GetTypeMark() and xTweener:GetControlByParent() then
            tmpXTweener:BeReplaced()
            break
        end
    end

    table.insert(self:GetListTweeners(), xTweener)
end

function XTweenObserver:RemoveTweener(xTweener)
    local t = self:GetListTweeners()
    for i = 1, #t do
        if t[i] == xTweener then
            table.remove(t, i)
            break
        end
    end
    ---改为不移除控件，避免再次创建的消耗
    --logError('#t  ' .. #t)
    --if #t == 0 then
    --    LuaComponent.DestroyImmediate(self.gameObject,self)
    --end
end

function XTweenObserver:OnEnable()
    local t = self:GetListTweeners()
    for i = 1, #t do

        if t[i] ~= nil then
            t[i]:OnEnable()
        end

    end
end

function XTweenObserver:OnDisable()
    local t = self:GetListTweeners()
    for i = 1, #t do

        if t[i] ~= nil then
            t[i]:OnDisable()
        end

    end
end

function XTweenObserver:OnDestroy()
    local t = self:GetListTweeners()
    for i = 1, #t do

        if t[i] ~= nil then
            t[i]:OnDestroy()
        end

    end
end


























