---@class XTweenerColor:XTweener
XTweenerColor = class(XTweener)

---@param colorType XTweenColorType
---@param target UnityEngine.Transform
---@param fromColor Color
---@param toColor Color
---@param duration float
---@param playTimes int
---@param pingPong bool
---@param curv XTweenCurvType
function XTweenerColor:Construction(colorType, target, fromColor, toColor, duration, playTimes, pingPong, curv)
    self:ConstructionCallBySubClass()
    if not XTweenCurv.CheckCurvExit(curv) then
        logError('添加的曲线类型不存在！！！！！curv=' .. tostring(curv))
        logError(debug.traceback())
    end
    self:SetTarget(target)

    --self.target = target
    self.fromColor = fromColor
    self.toColor = toColor
    self.colorType = colorType

    if colorType == XTweenColorType.Image then
        self.imageComponent = target:GetComponent("Image")
        if not self.imageComponent then
            logError("XTweenerColor 找不到 Image 控件")
        end
    elseif colorType == XTweenColorType.RawImage then
        self.rawImageComponent = target:GetComponent("RawImage")
        if not self.rawImageComponent then
            logError("XTweenerColor 找不到 RawImage 控件")
        end
    elseif colorType == XTweenColorType.Text then
        self.textComponent = target:GetComponent("Text")
        if not self.textComponent then
        end
    end


    --以下继承父类属性
    self.duration = duration
    self.curvType = curv
    self.pingPong = pingPong
    self.repeatTimes = playTimes

end

function XTweenerColor:Update(deltaTime)
    self:UpdateCallBySubClass(deltaTime)

    if self.beReplaced then
        self:XTweenerCompleteFinish()
        return
    end

    ---@type Color
    local color = Color.Lerp(self.fromColor, self.toColor, self.animationProgress)
    if self.colorType == XTweenColorType.Image and self.imageComponent then
        self.imageComponent.color = color
    elseif self.colorType == XTweenColorType.RawImage and self.rawImageComponent then
        self.rawImageComponent.color = color
    elseif self.colorType == XTweenColorType.Text and self.textComponent then
        self.textComponent.color = color
    end

    self:XTweenerCompleteFinish()

end

function XTweenerColor:OnPlay()
    if not self.startValue then
        if self.colorType == XTweenColorType.Image and self.imageComponent then
            self.startValue = self.imageComponent.color
        elseif self.colorType == XTweenColorType.RawImage and self.rawImageComponent then
            self.startValue = self.rawImageComponent.color
        elseif self.colorType == XTweenColorType.Text and self.textComponent then
            self.startValue = self.textComponent.color
        end
    end
end

function XTweenerColor:GetTypeMark()
    return 'XTweenerColor'
end