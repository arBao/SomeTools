---@class XTweenUpdater
XTweenUpdater = {}

local tweeners = {}

local function Update()

    local deltaTime = Time.GetDeltaTime()

    local tNeedRemove = {}
    for i = 1, #tweeners do
        local tweener = tweeners[i]
        if tweener:GetState() == XTweenerState.Playing then
            tweener:Update(deltaTime)
        end
        if tweener:GetState() == XTweenerState.End then
            table.insert(tNeedRemove, tweener)
        end
    end

    for i=1,#tNeedRemove do
        local needRemoveTweener = table.remove(tNeedRemove, 1)
        for j=1,#tweeners do
            if tweeners[j]==needRemoveTweener then
                table.remove(tweeners,j)
                break
            end
        end
    end
end

local bHasInit = false

function XTweenUpdater.AddTween(tweener)
    if not bHasInit then
        -- LateUpdateBeat:Add(Update, nil)
        UpdateBeat.Add(Update)
        bHasInit = true
    end
    table.insert(tweeners, tweener)
end


function XTweenUpdater.PrintLen()
    LogEditor('#tweeners=',#tweeners,LogColor.Red)
end

function XTweenUpdater.Clear()
    tweeners = {}
end