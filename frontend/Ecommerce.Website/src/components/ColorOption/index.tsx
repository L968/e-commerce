export interface ColorSvgProps {
    color: string,
    width: number,
    height: number,
}

export default function ColorSvg({ color, width, height }: ColorSvgProps) {
    return (
        <svg width={width} height={height} viewBox="0 0 17 16" fill="none" xmlns="http://www.w3.org/2000/svg">
            <circle cx="8.88464" cy="8" r="8" fill={color} />
        </svg>
    )
}
