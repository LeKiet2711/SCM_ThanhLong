namespace SCM_ThanhLong_Group.wwwroot.js
{
    public class slider
    {
        document.addEventListener("DOMContentLoaded", () => {
        const slides = document.querySelectorAll(".slide");
        let currentIndex = 0;

        function showNextSlide() {
            slides[currentIndex].classList.remove("active");
            currentIndex = (currentIndex + 1) % slides.length;
            slides[currentIndex].classList.add("active");
        }

        slides[currentIndex].classList.add("active");
        setInterval(showNextSlide, 3000);
    });

    }
}
