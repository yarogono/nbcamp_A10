# 팀 초보운전(A10조) - 카드 짝 맞추기 미니 팀 프로젝트
---

## 📆 프로젝트 기간
---

2023.08.07. (월) ~ 2023.08.10. (금)

<br/>

## 👥 팀 구성
---

|역할|이름|
|------|---|
|조장|기현빈([깃허브 프로필](https://github.com/homebd))|
|팀원|이정환([깃허브 프로필](https://github.com/jhwan328))|
|팀원|임전혁([깃허브 프로필](https://github.com/yarogono))|

<br/>

## 🎥 프로젝트 시연 영상
---

[![Video Label](http://img.youtube.com/vi/_m9tj4ETJpE/0.jpg)](https://youtu.be/_m9tj4ETJpE)

<br/>

## 와이어 프레임
---

![와이어프레임](https://github.com/homebd/nbcamp_A10/assets/70641418/4c6f8831-73cf-4b0f-86ea-69d347e67e27)

피그마를 사용해서 같이 와이어 프레임을 구상했습니다.
([피그마 링크](https://www.figma.com/file/Xvv6frZr2vJnMve2mIHLFc/Untitled?type=design&node-id=0%3A1&mode=design&t=DeubUwn7dfZSaGXh-1))

<br/>

## 구현한 주요 기능
---

![카드-분배](https://github.com/homebd/nbcamp_A10/assets/70641418/a5e99e3a-c935-4ed6-94ce-886aeaad3623)

카드 분배 기능
- 코루틴을 사용해서 각 카드가 순차적으로 분배 되도록 구현했습니다.
- 유니티 Vector3.Lerp() 를 사용해서 선형 보간법을 사용해 오브젝트를 부드럽게 이동하도록 했습니다.
- 유니티 애니메이션 기능을 사용해서 카드 분배 시 회전과 스케일이 줄어들었다 커지도록 했습니다.

<br/>

![스테이지](https://github.com/homebd/nbcamp_A10/assets/70641418/05cc4a58-cdb9-457a-83f0-06587f103f1f)

카드 게임 스테이지 기능
- 스테이지를 만들어서 난이도를 설정했습니다. 
- 스테이지 해금 기능을 만들었습니다.

<br/>

## 구현 중 발생한 버그
---



<br/>

