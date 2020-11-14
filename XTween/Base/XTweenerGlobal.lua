---@class XTweenerState
XTweenerState = {

    BeforePlay = 'BeforePlay',
    Playing = 'Playing',
    Pause = 'Pause',
    End = 'End',

}

---@class XTweenCurvType
XTweenCurvType = {

    Linear = 'Linear',
    InSine = 'InSine',
    OutSine = 'OutSine',
    InOutSine = 'InOutSine',
    --InQuad = 'InQuad',
    OutQuad = 'OutQuad',
    --InOutQuad = 'InOutQuad',
    --InCubic = 'InCubic',
    OutCubic = 'OutCubic',
    --InOutCubic = 'InOutCubic',
    --InQuart = 'InQuart',
    OutQuart = 'OutQuart',
    --InOutQuart = 'InOutQuart',
    --InQuint = 'InQuint',
    --OutQuint = 'OutQuint',
    --InOutQuint = 'InOutQuint',
    --InExpo = 'InExpo',
    --OutExpo = 'OutExpo',
    --InOutExpo = 'InOutExpo',
    --InCirc = 'InCirc',
    OutCirc = 'OutCirc',
    --InOutCirc = 'InOutCirc',
    --InElastic = 'InElastic',
    --OutElastic = 'OutElastic',
    --InOutElastic = 'InOutElastic',
    InBack = 'InBack',
    OutBack = 'OutBack',
    --InOutBack = 'InOutBack',
    --InBounce = 'InBounce',
    OutBounce = 'OutBounce',
    --InOutBounce = 'InOutBounce',
    --Flash = 'Flash',
    --InFlash = 'InFlash',
    --OutFlash = 'OutFlash',
    --InOutFlash = 'InOutFlash',
    EaseIn = 'EaseIn',
    EaseOut = 'EaseOut',
    EaseInOut = 'EaseInOut',
    BounceIn = 'BounceIn',
    BounceOut = 'BounceOut',
    BounceInOut = 'BounceInOut',
    BuyuGold1 = 'BuyuGold1',
    Lettory1 = "Lettory1",
}

---@class XTweenGroupAddMode
XTweenGroupAddMode = {
    Queue = 'Queue', --当前队列位置插入新的队列元素
    Parallel = 'Parallel', --当前队列位置并行执行
}

---@class XTweenMoveType
XTweenMoveType = {
    WorldPosition = 'WorldPosition',
    LocalPosition = 'LocalPosition',
    AnchoredPosition = 'AnchoredPosition',
}

---@class XTweenRotationType
XTweenRotationType = {
    WorldRotation = 'WorldRotation',
    LocalRotation = 'LocalRotation',
}

---@class XTweenColorType
XTweenColorType = {
    Image = 'Image',
    RawImage = 'RawImage',
    Text = 'Text',
}
